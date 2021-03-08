using System.Net;
using Newtonsoft.Json.Linq;

namespace AfterRefactor.Infrastructure
{
    public class HttpResult
    {
        public HttpResult(bool success, JObject body, HttpStatusCode status)
        {
            Success = success;
            Body = body;
            Status = status;
        }

        public bool Success { get;  }
        public JObject Body { get;  }
        public HttpStatusCode Status { get; }
    }
}