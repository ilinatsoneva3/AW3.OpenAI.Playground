using System.Net;

namespace AW3.GR.OpenAI.Application.Common.Errors;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }

    public string Message { get; }
}
