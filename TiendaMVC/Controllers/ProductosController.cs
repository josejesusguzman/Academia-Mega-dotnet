using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;
using TiendaMVC.Services;
using System.Collections.Generic;

namespace TiendaMVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoApiService _api;
        public ProductosController(IProductoApiService api) => _api = api;

        //GET /Productos
        public async Task<IActionResult> Index()
        {
            var products = await _api.GetAllAsync();
            return View(products);
        }

        // GET Productos/Create
        public IActionResult Create() -> View();

        // POST /Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            
        }

        public IActionResult Details(int id)
        {
            return View(new Producto());
        }

    }
}