namespace BeforeRefactor.Dtos
{
    public class Customer
    {
        public int Id { get; }
        public string Name { get; }
        public long Amount { get; }
        
        public Customer(int id, string name, long amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }
    }
}
