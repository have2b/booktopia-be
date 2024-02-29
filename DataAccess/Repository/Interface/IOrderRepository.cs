using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface;

public interface IOrderRepository
{
    Task<List<Order>> GetOrders(RequestDTO input);
    Task<Order> AddOrder(OrderDTO model);
}