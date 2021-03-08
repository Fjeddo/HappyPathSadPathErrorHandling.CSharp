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

        public async Task<ServiceResult<Customer>> GetCustomerById(int id)
        {
            var response = await _httpService.Get($"https://reqres.in/api/users/{id}");

            if (response.Success)
            {
                return new ServiceResult<Customer>(0, new Customer(response.Body));
            }

            _log.LogWarning("Something did not work out correctly");
            return new ServiceResult<Customer>((int) response.Status, null);
        }
    }
}
