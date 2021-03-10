using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace AfterRefactor.Infrastructure
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpService> _log;

        public HttpService(HttpClient httpClient, ILogger<HttpService> log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<(bool success, JObject body, HttpStatusCode statusCode)> Get(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                var body = JObject.Parse(await response.Content.ReadAsStringAsync());
                var statusCode = response.StatusCode;
                var success = response.IsSuccessStatusCode;

                return (success, body, statusCode);
            }
            catch (InvalidOperationException e)
            {
                _log.LogError("Returning a HttpStatusCode.BadRequest", e);

                return ErrorResult(HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException e)
            {
                _log.LogError("Returning a HttpStatusCode.BadGateway", e);

                return ErrorResult(HttpStatusCode.BadGateway);
            }
            catch (TaskCanceledException e)
            {
                _log.LogError("Returning a HttpStatusCode.RequestTimeout", e);

                return ErrorResult(HttpStatusCode.RequestTimeout);
            }
            catch (Exception e)
            {
                _log.LogError("Returning a HttpStatusCode.InternalServerError", e);

                return ErrorResult(HttpStatusCode.InternalServerError);
            }
        }

        private static (bool, JObject, HttpStatusCode) ErrorResult(HttpStatusCode statusCode) => (false, null, statusCode);
    }
}
