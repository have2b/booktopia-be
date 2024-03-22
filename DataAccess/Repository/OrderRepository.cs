using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDAO _dao = OrderDAO.Instance;
    public Task<List<Order>> GetOrders(RequestDTO input) => _dao.GetOrdersAsync(input);

    public Task<Order> AddOrder(string userName, OrderDTO model) => _dao.AddOrderASync(userName, model);

    public Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate) => _dao.GetOrdersByDateRange(startDate, endDate);

    public Task<List<Order>> GetOrdersByYear(int year) => _dao.GetOrdersByYear(year);
    public Task<List<Order>> GetRecentOrders(RequestDTO input) => _dao.GetRecentOrders(input);

}