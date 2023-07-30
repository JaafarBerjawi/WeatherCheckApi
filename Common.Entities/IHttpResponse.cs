using System.Net;

namespace Common.Entities
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; set; }
    }
}
