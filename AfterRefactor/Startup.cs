using AfterRefactor.Infrastructure;
using AfterRefactor.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AfterRefactor.Startup))]
namespace AfterRefactor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddSingleton<CustomerService>();
        }
    }
}
