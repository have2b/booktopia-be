using BusinessObject.DTO;
using BusinessObject.Model;
using static BusinessObject.Model.Order;

namespace DataAccess.Repository.Interface;

public interface IOrderRepository
{
    Task<List<OrderInfoDTO>> GetOrders(RequestDTO input, bool? latest);
    Task<OrderInfoDTO> GetOrderByOrderId(int orderId);

    Task<List<OrderDetailInfoDTO>> GetOrderDetailByOrderId(int orderId);

    Task<Order> AddOrder(string userName, OrderDTO model);
    Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
    Task<List<Order>> GetOrdersByYear(int year);
    Task<List<Order>> GetRecentOrders(RequestDTO input);
    Task<Order> UpdateOrderStatus(int orderId, StatusType status);

}