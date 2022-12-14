using Microsoft.EntityFrameworkCore;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TakeMeHome.API.TakeMeHome.Domain.Repositories;
using TakeMeHome.API.TakeMeHome.Persistence.Contexts;

namespace TakeMeHome.API.TakeMeHome.Persistence.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _context.Products
            .Include(p => p.Order)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product> FindByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IEnumerable<Product>> FindByUserIdAndStatusIdAsync(int userId, int statusId)
    {
        return await _context.Products
            .Where(p => p.Order.UserId == userId && p.Order.OrderStatusId == statusId)
            .Include(p=> p.Order)
            .ToListAsync();
    }

    public async Task<Product> FindByOrderIdAsync(int orderId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}