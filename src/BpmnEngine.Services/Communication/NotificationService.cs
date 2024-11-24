using System.Net.Mail;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Storage.Abstractions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Communication;

public class NotificationService : INotificationService
{
    private readonly IFormsRepository _formsRepository;
    private readonly IUserActionsRepository _repository;
    private readonly SmtpOptions _options;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IFormsRepository formsRepository, IUserActionsRepository repository,
        SmtpOptions options,
        ILogger<NotificationService> logger)
    {
        _formsRepository = formsRepository;
        _repository = repository;
        _options = options;
        _logger = logger;
    }

    public async Task<bool> ConfirmSavedProcessId(Guid id)
    {
        var exists = await _formsRepository.ExecutedProcessById(id);

        return exists;
    }

    public async Task<bool> SendNotificationAsync(Guid actionId, Guid processId, 
        string topicName,
        string userName, CancellationToken cancellationToken)
    {
        var ix = await _repository.InsertUserActionAsync(actionId, processId, topicName, cancellationToken);

        if (ix > 0)
        {
            var body = $"Użytkownik {userName} złożył wniosek.<br/><a href='https://localhost:7000/processes/decision/{actionId}'>Link do podjęcia decyzji.</a>";

            _logger.LogInformation(body);

            SendMail($"Powiadomienie z Camunda. Decyzja: {actionId}", body);
            return true;
        }

        return false;
    }

    public bool InformSenderAccepted(string businessKey)
    {
        SendMail("Powiadomienie z Camunda. Wniosek został zaakceptowany", $"{businessKey} został zaakceptowany");

        return true;
    }

    public bool InformSenderRejected(string businessKey)
    {
        SendMail("Powiadomienie z Camunda. Wniosek został odrzucony", $"{businessKey} został odrzucony");

        return true;
    }

    private void SendMail(string subject, string body)
    {
        try
        {
            var mailObj = new MailMessage(_options.From, _options.To, subject, body);
            mailObj.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_options.Host, _options.Port);

            smtpClient.Send(mailObj);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendMail));

            throw;
        }
    }
}