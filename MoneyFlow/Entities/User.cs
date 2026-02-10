namespace MoneyFlow.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public  required string Email { get; set; }
        public required string Password { get; set; }

        public ICollection<Service> Services { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = null!;
    }
}
