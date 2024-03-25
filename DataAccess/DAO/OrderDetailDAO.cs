using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public class OrderDetailDAO
{
    private readonly BookDAO _bookDao = BookDAO.Instance;
    private static readonly Lazy<OrderDetailDAO> _instance = new(() => new OrderDetailDAO(new AppDbContext()));

    private readonly AppDbContext _context;

    private OrderDetailDAO(AppDbContext context)
    {
        _context = context;
    }

    public static OrderDetailDAO Instance => _instance.Value;

    // Create a list of order detail when send whole cart from FE

    public async Task AddOrderDetailsAsync(List<OrderDetailDTO> list)
    {
        var mapper = MapperConfig.Init();
        try
        {
            foreach (var orderDetail in list)
            {
                var od = mapper.Map<OrderDetail>(orderDetail);
                var book = await _bookDao.GetBookByIdAsync(orderDetail.BookId);
                book.Quantity -= orderDetail.Quantity;
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                await _context.OrderDetails.AddAsync(od);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Still error but don't know why =)))");
        }
    }

    public async Task<List<OrderDetailInfoDTO>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        var orderDetails = await _context.OrderDetails.Include(x => x.Book).ThenInclude(x => x.Publisher).Where(x => x.OrderId == orderId)
            .ToListAsync();
        return orderDetails.Select(x => new OrderDetailInfoDTO
        {
            OrderId = x.OrderId,
            BookId = x.BookId,
            Discount = x.Discount,
            Quantity = x.Quantity,
            BookName = x.Book?.BookName,
            Author = x.Book?.Author,
            ImageUrl = x.Book?.ImageUrl,
            PublisherName = x.Book?.Publisher?.PublisherName,
            SellPrice = x.Book?.SellPrice ?? 0,
        }).ToList();
    }
}