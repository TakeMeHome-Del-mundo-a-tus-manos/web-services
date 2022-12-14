using TakeMeHome.API.TakeMeHome.Domain.Models;

namespace TakeMeHome.API.TakeMeHome.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> ListAsync();
    Task AddAsync(Order order);
    Task<Order> FindByIdAsync(int id);
    Task<IEnumerable<Order>> FindByOrderStatusId(int orderStatusId);
    Task<IEnumerable<Order>> FindyByUserId(int userId);
    Task<IEnumerable<Order>> FindByStatusIdAndUserId(int orderStatusId, int userId);
    Task<IEnumerable<Order>> FindByOrderStatusIdAndUserId(int orderStatusId, int userId);
    //Task<IEnumerable<Order>> FindyByUserId(int userId);
    Task<Order> FindByOrderCodeAndUserId(string orderCode, int userId);
    void Update(Order order);
    void Remove(Order order);
}