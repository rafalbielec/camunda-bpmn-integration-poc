using System.Diagnostics.CodeAnalysis;
using System.Net;
using BpmnEngine.Camunda.Client.Responses;

namespace BpmnEngine.Camunda.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ClientException : Exception
{
    private readonly ErrorResponse _errorResponse;

    public ClientException(ErrorResponse errorResponse, HttpStatusCode statusCode)
    {
        _errorResponse = errorResponse;
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
    public string ErrorType => _errorResponse.Type;
    public string ErrorMessage => _errorResponse.Message;
    public override string Message => $"Camunda error of type \"{ErrorType}\" with message \"{ErrorMessage}\"";
}