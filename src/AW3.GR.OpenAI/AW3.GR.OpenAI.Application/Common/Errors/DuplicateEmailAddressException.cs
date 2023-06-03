using System.Net;

namespace AW3.GR.OpenAI.Application.Common.Errors;

public class DuplicateEmailAddressException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string Message => "Email address already exists";
}
