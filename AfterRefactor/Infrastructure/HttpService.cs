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
        
        public async Task<HttpResult> Get(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                var body = JObject.Parse(await response.Content.ReadAsStringAsync());
                var status = response.StatusCode;
                var success = response.IsSuccessStatusCode;

                return new HttpResult(success, body, status);
            }
            catch (InvalidOperationException e)
            {
                _log.LogError("Returning a HttpStatusCode.BadRequest", e);
                
                return new HttpResult(false, null, HttpStatusCode.BadRequest);
            }
            catch (HttpRequestException e)
            {
                _log.LogError("Returning a HttpStatusCode.BadGateway", e);
                
                return new HttpResult(false, null, HttpStatusCode.BadGateway);
            }
            catch (TaskCanceledException e)
            {
                _log.LogError("Returning a HttpStatusCode.RequestTimeout", e);
                
                return new HttpResult(false, null, HttpStatusCode.RequestTimeout);
            }
            catch (Exception e)
            {
                _log.LogError("Returning a HttpStatusCode.InternalServerError", e);
                
                return new HttpResult(false, null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
