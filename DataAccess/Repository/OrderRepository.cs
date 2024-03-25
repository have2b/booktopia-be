using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDAO _dao = OrderDAO.Instance;
    public Task<List<OrderInfoDTO>> GetOrders(RequestDTO input, bool? latest) => _dao.GetOrdersAsync(input, latest);

    public Task<Order> AddOrder(string userName, OrderDTO model) => _dao.AddOrderASync(userName, model);

    public Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate) => _dao.GetOrdersByDateRange(startDate, endDate);

    public Task<List<Order>> GetOrdersByYear(int year) => _dao.GetOrdersByYear(year);
    public Task<List<Order>> GetRecentOrders(RequestDTO input) => _dao.GetRecentOrders(input);

    public Task<List<OrderDetailInfoDTO>> GetOrderDetailByOrderId(int orderId) => _dao.GetOrderDetailByOrderId(orderId);

    public Task<OrderInfoDTO> GetOrderByOrderId(int orderId) => _dao.GetOrderByOrderIdAsync(orderId);

    public Task<OrderInfoDTO> UpdateOrderStatus(int orderId, Order.StatusType status) => _dao.UpdateOrderStatus(orderId, status);
    public Task<List<OrderInfoDTO>> GetOrdersByUserId(string userName) => _dao.GetOrdersByUserId(userName);
}