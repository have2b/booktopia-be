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

    // Get book count
    public async Task<int> GetBookCount()
    {
        return await _context.Books.CountAsync();
    }

    // Search book
    public async Task<List<Book>> SearchBook(SearchBookDTO model)
    {
        var query = _context.Books.Include(b => b.Category).Include(b => b.Publisher).AsQueryable();

        if (!string.IsNullOrEmpty(model.BookName))
            query = query.Where(b => b.BookName.Contains(model.BookName));

        if (!string.IsNullOrEmpty(model.CategoryId) && int.TryParse(model.CategoryId, out var catId))
            query = query.Where(b => b.CategoryId == catId);

        if (!string.IsNullOrEmpty(model.Author))
            query = query.Where(b => b.Author.Contains(model.Author));

        if (!string.IsNullOrEmpty(model.PublisherName))
            query = query.Where(b => b.Publisher.PublisherName.Contains(model.PublisherName));

        return await query.ToListAsync();
    }

    // Get all books
    public async Task<List<Book>> GetBooksAsync(RequestDTO input, bool? latest, string? sort)
    {
        IQueryable<Book> query = _context.Books;

        if (latest is true)
        {
            return await query
                .OrderByDescending(b => b.CreatedAt)
                .Take(20)
                .ToListAsync();
        }

        switch (sort)
        {
            case "nameDesc":
                query = query.OrderByDescending(b => b.BookName);
                break;
            case "nameAsc":
                query = query.OrderBy(b => b.BookName);
                break;
            case "priceHigh":
                query = query.OrderByDescending(b => b.SellPrice);
                break;
            case "priceLow":
                query = query.OrderBy(b => b.SellPrice);
                break;
        }

        return await query
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