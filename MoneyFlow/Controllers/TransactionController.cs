using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.DTOs;
using MoneyFlow.Managers;
using System.Security.Claims;
using System.Transactions;

namespace MoneyFlow.Controllers
{
    [Authorize]
    public class TransactionController(ServiceManager _serviceManager, Managers.TransactionManager _transactionManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ServicesByType(string typeService)
        {
            //TODO: Get userId from session or authentication context
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userIdInt = int.Parse(userId);
            var services = _serviceManager.GetByType(userIdInt, typeService);
            return Ok(services);
        }

        [HttpPost]
        public IActionResult New([FromBody] TransactionDTO modelDTO)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            modelDTO.UserId = int.Parse(userId); //TODO: Get userId from session or authentication context
            var response = _transactionManager.New(modelDTO);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult HistoryTransaction(DateOnly startDate, DateOnly endDate)
        {
            //TODO: Get userId from session or authentication context
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var list = _transactionManager.GetHistory(startDate, endDate, int.Parse(userId));
            return Ok(new { data = list });
        }

        public IActionResult History()
        {
            return View();
        }
    }
}
