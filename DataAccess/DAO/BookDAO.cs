using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public class BookDAO
{
    private static readonly Lazy<BookDAO> _instance = new(() => new BookDAO(new AppDbContext()));

    private readonly AppDbContext _context;

    private BookDAO(AppDbContext context)
    {
        _context = context;
    }

    public static BookDAO Instance => _instance.Value;
    
    // Get all books
    public async Task<List<Book>> GetBooksAsync(RequestDTO input, bool? latest)
    {
        if (latest is true)
        {
            return await _context.Books
                .OrderByDescending(b=> b.CreatedAt)
                .Take(20)
                .ToListAsync();
        }
        return await _context.Books
            .Skip(input.PageIndex * input.PageSize)
            .Take(input.PageSize)
            .ToListAsync();
    }
    
    // Get a book by Id
    public async Task<Book> GetBookByIdAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        if (book == null) throw new BookNotFoundException(id);
        return book;
    }
    
    // Add new book
    public async Task<Book> AddBookASync(BookDTO model)
    {
        var mapper = MapperConfig.Init();
        var book = mapper.Map<Book>(model);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    // Update a book (just with basic information, not quantity)
    public async Task<Book> UpdateBookAsync(int id, BookDTO model)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        if (book == null) throw new BookNotFoundException(id);
        
        var mapper = MapperConfig.Init();
        book = mapper.Map(model, book);
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    // Delete a book
    public async Task<Book> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        if (book == null) throw new BookNotFoundException(id);
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    // Import for specific book
    public async Task<Book> ImportBookAsync(int id, int quantity)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        if (book == null) throw new BookNotFoundException(id);
        book.Quantity += quantity;
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    // Sell book
    public async Task<Book> SellBookAsync(int id, int quantity)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        if (book == null) throw new BookNotFoundException(id);
        book.Quantity -= quantity;
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
}