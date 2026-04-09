using Microsoft.AspNetCore.Mvc;
using InventaCore.Models;

namespace InventaCore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Username == "admin" && model.Password == "1234")
            {
                HttpContext.Session.SetString("Usuario", model.Username);
                return RedirectToAction("Index", "Productos");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}