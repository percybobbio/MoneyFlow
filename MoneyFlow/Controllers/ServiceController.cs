using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Context;
using MoneyFlow.Entities;
using MoneyFlow.Managers;
using MoneyFlow.Models;
using System.Security.Claims;

namespace MoneyFlow.Controllers
{
    [Authorize]
    public class ServiceController(ServiceManager _serviceManager) : Controller
    {
        public IActionResult Index()
        {
            //TODO: cambiar el userId por el del usuario logueado
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var list = _serviceManager.GetAll(int.Parse(userId));
            return View(list);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(ServiceVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            model.UserId = int.Parse(userId); //TODO: cambiar el userId por el del usuario logueado
            var response = _serviceManager.New(model);
            if (response == 1)
            {
                return RedirectToAction("Index");
            }
            //Viewbag permite enviar datos a la vista
            ViewBag.message = "Error";
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _serviceManager.GetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ServiceVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = _serviceManager.Edit(model);
            if (response == 1)
            {
                return RedirectToAction("Index");
            }
            ViewBag.message = "Error";
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var response = _serviceManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
