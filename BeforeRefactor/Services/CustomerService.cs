using System;
using System.Net.Http;
using System.Threading.Tasks;
using BeforeRefactor.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BeforeRefactor.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _log;

        public CustomerService(HttpClient httpClient, ILogger<CustomerService> log)
        {
            _httpClient = httpClient;
            _log = log;
        }
        
        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://reqres.in/api/users/{id}");
            
                if (response.IsSuccessStatusCode)
                {
                    var body = JObject.Parse(await response.Content.ReadAsStringAsync());
                    return new Customer(id, $"{body["data"]["first_name"]} {body["data"]["last_name"]}", 0);
                }

                _log.LogWarning("Something did not work out correctly");
                return null;
            }
            catch (Exception e)
            {
                _log.LogError(e, "Something blew up");
                throw;
            }
        }
    }
}
