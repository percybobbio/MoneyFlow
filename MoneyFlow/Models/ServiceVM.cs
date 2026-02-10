namespace MoneyFlow.Models
{
    public class ServiceVM
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }

    }
}
