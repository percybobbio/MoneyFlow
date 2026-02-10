namespace MoneyFlow.DTOs
{
    public class HistoryDTO
    {
        public string Date { get; set; }
        public string Month { get; set; }
        public string TypeService { get; set; }
        public string ServiceName { get; set; }
        public string Comment { get; set; }
        public decimal TotalAmount { get; set; }


    }
}
