using TiendaMVC.Models;

namespace TiendaMVC.Services
{
    public interface IProductoApiService
    {
        Task<List<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<Producto?> CreateAsync(Producto p);
        Task<bool> UpdateAsync(int id, Producto p);
        Task<bool> DeleteAsync(int id); 

    }
}
