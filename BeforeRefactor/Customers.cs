using System;
using System.Threading.Tasks;
using System.Web.Http;
using BeforeRefactor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BeforeRefactor
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

            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return new NotFoundResult();
                }
            
                return new OkObjectResult(customer);
            }
            catch (Exception e)
            {
                log.LogError(e, "Catching a exception");
                return new InternalServerErrorResult();
            }
        }
    }
}
