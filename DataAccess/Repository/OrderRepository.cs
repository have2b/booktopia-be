using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDAO _dao = OrderDAO.Instance;
    public Task<List<Order>> GetOrders(RequestDTO input) => _dao.GetOrdersAsync(input);

    public Task<Order> AddOrder(OrderDTO model) => _dao.AddOrderASync(model);
}