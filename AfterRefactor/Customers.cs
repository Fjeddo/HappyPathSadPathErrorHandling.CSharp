using System.Threading.Tasks;
using AfterRefactor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AfterRefactor
{
    public class Customers
    {
        private readonly CustomerService _customerService;

        public Customers(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [FunctionName("GetCustomer")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "api/customers/{id}")] HttpRequest req, int id, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var (customer, status) = await _customerService.GetCustomerById(id);

            return status == 0 
                ? new OkObjectResult(customer) 
                : new StatusCodeResult(status);
        }
    }
}
