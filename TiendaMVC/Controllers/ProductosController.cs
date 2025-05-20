using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;
using System.Collections.Generic;

namespace TiendaMVC.Controllers
{
    public class ProductosController : Controller
    {
        private static readonly List<Producto> _productos = new()
        {
            new Producto { Id = 1, Nombre = "Xiaomi 15 Ultra", Precio = 33000.00m},
            new Producto { Id = 2, Nombre = "HONOR Magic 7 Pro", Precio = 29000.00m}
        };

        public IActionResult Index()
        {
            return View(_productos);
        }

        public IActionResult Details(int id)
        {
            var product = _productos.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

    }
}