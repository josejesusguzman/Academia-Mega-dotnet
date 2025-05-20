using Microsoft.AspNetCore.Mvc;
using PrimeraAPI.Models;
using PrimeraAPI.Data;

namespace PrimeraAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/productos
    public class ProductosController : ControllerBase 
    {
        // Aquí sería la lectura de datos en BD 
        private readonly ProductoService _service;
        public ProductosController(ProductoService service)
        {
            _service = service;
        }

        /*
                - CREATE - 
        */
        [HttpPost] // POST /api/productos
        public async Task<IActionResult> Create(Producto nuevo)
        {
            var created = await _service.CreateAsync(nuevo);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /*
                - READ - 
        */

        [HttpGet] // GET /api/productos
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")] // GET /api/productos/1
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);

        }

        /*
                - UPDATE - 
        */
        [HttpPut("{id}")] // PUT /api/productos/1
        public async Task<IActionResult> Update(int id, Producto actualizado)
        {
            var updated = await _service.UpdateAsync(id, actualizado);
            if (!updated) return NotFound();
            return NoContent();
        }

        /*
                - DELETE -
        */
        [HttpDelete("{id}")] // DELETE /api/productos/1
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }

    }
}


