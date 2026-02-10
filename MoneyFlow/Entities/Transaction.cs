namespace MoneyFlow.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public required string Comment { get; set; }
        public required DateOnly Date { get; set; }
        public required decimal TotalAmount { get; set; }

        public Service Service { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
