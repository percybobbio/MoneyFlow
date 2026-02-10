namespace MoneyFlow.Models
{
    public class UserVM
    {
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string RepeatPassword { get; set; }
    }
}
