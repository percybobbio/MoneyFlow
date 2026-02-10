using MoneyFlow.Context;
using MoneyFlow.DTOs;
using MoneyFlow.Entities;

namespace MoneyFlow.Managers
{
    public class TransactionManager(AppDbContext _dbContext)
    {
        public int New(TransactionDTO modelDTO)
        {
            var entity = new Transaction
            {
                Date = modelDTO.Date,
                Comment = modelDTO.Comment, 
                TotalAmount = modelDTO.TotalAmount,
                ServiceId = modelDTO.ServiceId,
                UserId = modelDTO.UserId
            };

            _dbContext.Transactions.Add(entity);
            var rowsAfected = _dbContext.SaveChanges();
            return rowsAfected;
        }

        public List<HistoryDTO> GetHistory(DateOnly startDate, DateOnly EndDate, int UserId)
        {
            var list = _dbContext.Transactions
                .Where(item =>
                item.UserId == UserId &&
                item.Date >= startDate && item.Date <= EndDate
                ).Select(item => new HistoryDTO
                {
                    Date = item.Date.ToString("dd/MM/yyyy"),
                    Month = item.Date.ToString("MMMM"),
                    TypeService = item.Service.Type,
                    ServiceName = item.Service.Name,
                    Comment = item.Comment,
                    TotalAmount = item.TotalAmount
                }).ToList();

            return list;
        }
    }
}
