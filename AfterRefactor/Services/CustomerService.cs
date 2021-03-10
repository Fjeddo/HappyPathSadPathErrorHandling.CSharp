using System.Threading.Tasks;
using AfterRefactor.Dtos;
using AfterRefactor.Infrastructure;
using Microsoft.Extensions.Logging;

namespace AfterRefactor.Services
{
    public class CustomerService
    {
        private readonly HttpService _httpService;
        private readonly ILogger _log;

        public CustomerService(HttpService httpService, ILogger<CustomerService> log)
        {
            _httpService = httpService;
            _log = log;
        }

        public async Task<(Customer customer, int status)> GetCustomerById(int id)
        {
            var (success, body, statusCode) = await _httpService.Get($"https://reqres.in/api/users/{id}");
            
            if (success)
            {
                return _(new Customer(body));
            }

            _log.LogWarning("Something did not work out correctly");

            return _((int)statusCode);
        }

        static (Customer customer, int status) _(int status) => (null, status);
        static (Customer customer, int status) _(Customer customer) => (customer, 0);
    }
}
