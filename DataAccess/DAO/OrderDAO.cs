﻿using BusinessObject;
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
    public async Task<List<Order>> GetOrdersAsync(RequestDTO input)
    {
        return await _context.Orders
            .Skip(input.PageIndex * input.PageSize)
            .Take(input.PageSize)
            .ToListAsync();
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
}