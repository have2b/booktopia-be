using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface;

public interface IOrderRepository
{
    Task<List<Order>> GetOrders(RequestDTO input);
    Task<Order> AddOrder(string userName, OrderDTO model);
    Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
    Task<List<Order>> GetOrdersByYear(int year);
    Task<List<Order>> GetRecentOrders(RequestDTO input);
}