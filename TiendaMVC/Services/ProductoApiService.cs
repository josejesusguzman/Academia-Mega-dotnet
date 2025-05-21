using System.Net.Http.Json;
using TiendaMVC.Models;

namespace TiendaMVC.Services
{
    public class ProductoApiService : IProductoApiService
    {

        private readonly HttpClient _http;
        public ProductoApiService(HttpClient http) => _http = http;

        public async Task<List<Producto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<Producto>>("api/productos") ?? new List<Producto>();

        public async Task<Producto?> CreateAsync(Producto p)
        {
            var response = await _http.PostAsJsonAsync("api/productos", p);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<Producto>();
        }

    }
}