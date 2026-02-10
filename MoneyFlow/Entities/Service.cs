namespace MoneyFlow.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }

        public required User User { get; set; }
    }
}
