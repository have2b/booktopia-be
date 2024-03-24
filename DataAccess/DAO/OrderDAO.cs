using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public class OrderDAO
{
    private readonly OrderDetailDAO _orderDetailDao = OrderDetailDAO.Instance;
    private readonly UserDAO _userDao = UserDAO.Instance;
    private static readonly Lazy<OrderDAO> _instance = new(() => new OrderDAO(new AppDbContext()));

    private readonly AppDbContext _context;

    private OrderDAO(AppDbContext context)
    {
        _context = context;
    }

    public static OrderDAO Instance => _instance.Value;

    // Get all order
    public async Task<List<OrderInfoDTO>> GetOrdersAsync(RequestDTO input, bool? latest)
    {
        var orders = new List<Order>();
        if (latest is true)
        {
            orders = await _context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Book)
                .OrderByDescending(o => o.CreatedAt)
                .Skip(input.PageIndex * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

        }
        else
        {
            orders = await _context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Book)
                .Skip(input.PageIndex * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();
        }
        return orders.Select(x => ConvertOrderToOrderInfoDTO(x)).ToList();
    }

    // Create order and order detail
    public async Task<Order> AddOrderASync(string userName, OrderDTO model)
    {
        var flag = true;
        var user = await _userDao.GetFullfilUserByUserNameAsync(userName);
        // Check quantity of book
        foreach (var orderDetail in model.OrderDetails)
        {
            var book = await _context.Books.FindAsync(orderDetail.BookId);
            if (book == null) throw new BookNotFoundException(orderDetail.BookId);
            if (book.Quantity >= orderDetail.Quantity) continue;
            flag = false;
            throw new InsufficientQuantityException(book.BookName);
        }

        if (flag)
        {
            var mapper = MapperConfig.Init();
            var order = mapper.Map<Order>(model);
            order.UserId = user.Id;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var orderDetail in model.OrderDetails)
            {
                orderDetail.OrderId = order.OrderId;
            }

            await _orderDetailDao.AddOrderDetailsAsync(model.OrderDetails);
            return order;
        }

        return null;
    }

    public async Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Orders.Include(x => x.OrderDetails).ThenInclude(y => y.Book)
            .Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate).ToListAsync();
    }

    public async Task<List<Order>> GetOrdersByYear(int year)
    {
        return await _context.Orders.Include(x => x.OrderDetails).ThenInclude(y => y.Book)
            .Where(x => x.CreatedAt.Year == year).ToListAsync();
    }

    public async Task<List<Order>> GetRecentOrders(RequestDTO input)
    {
        return await _context.Orders.Include(x => x.OrderDetails).ThenInclude(y => y.Book).Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(input.PageIndex * input.PageSize)
            .Take(input.PageSize)
            .ToListAsync();
    }

    public async Task<List<OrderDetailInfoDTO>> GetOrderDetailByOrderId(int orderId)
    {
        return await _orderDetailDao.GetOrderDetailsByOrderIdAsync(orderId);
    }
    public async Task<OrderInfoDTO> GetOrderByOrderIdAsync(int orderId)
    {
        var order = await _context.Orders
             .Include(x => x.User)
            .Include(x => x.OrderDetails).ThenInclude(x => x.Book)
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
        return ConvertOrderToOrderInfoDTO(order);
    }

    public async Task<OrderInfoDTO> UpdateOrderStatus(int orderId, Order.StatusType status)
    {
        var order = await _context.Orders
               .Include(x => x.User)
            .Include(x => x.OrderDetails).ThenInclude(x => x.Book)
            .FirstOrDefaultAsync(b => b.OrderId == orderId);
        if (order == null) throw new OrderNotFoundException(orderId);
        order.Status = status;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return ConvertOrderToOrderInfoDTO(order);
    }

    public decimal GetRevenueByOrder(Order order)
    {
        return order.OrderDetails.Sum(y => y.Quantity * y.Book.SellPrice * (1 - (decimal)y.Discount / 100));
    }

    public OrderInfoDTO ConvertOrderToOrderInfoDTO(Order order)
    {
        return new OrderInfoDTO
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            UserId = order.UserId,
            Name = order.User?.Name,
            PhoneNumber = order.User?.PhoneNumber,
            Email = order.User?.Email,
            ShipAddress = order.ShipAddress,
            SaleAmount = GetRevenueByOrder(order)
        };
    }

}