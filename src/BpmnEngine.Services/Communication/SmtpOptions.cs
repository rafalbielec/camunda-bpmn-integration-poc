namespace BpmnEngine.Services.Communication;

public class SmtpOptions
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string To { get; set; }
    public string From { get; set; }
}