using Newtonsoft.Json.Linq;

namespace AfterRefactor.Dtos
{
    public class Customer
    {
        public int Id { get; }
        public string Name { get; }
        public long Amount { get; }
       
        public Customer(JObject jsonCustomerObject)
        {
            Id= jsonCustomerObject["id"].Value<int>();
            Name = $"{jsonCustomerObject["data"]["first_name"]} {jsonCustomerObject["data"]["last_name"]}";
            Amount = 0;
        }
    }
}
