using MoneyFlow.Entities;

namespace MoneyFlow.DTOs
{
    public class TransactionDTO
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public required string Comment { get; set; }
        public required DateOnly Date { get; set; }
        public required decimal TotalAmount { get; set; }

    }
}
