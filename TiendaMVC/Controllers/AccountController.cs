using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;
using TiendaMVC.Services;


namespace TiendaMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiClient _api;
        public AccountController(ApiClient api) => _api = api;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid) return View(user);
            var valido = await _api.LoginAsync(user);
            if (!valido)
            {
                ModelState.AddModelError("", "Usuario o contraseña no validos");
                return View(user);
            }
            return RedirectToAction("Index", "Productos");
        }


    }
}