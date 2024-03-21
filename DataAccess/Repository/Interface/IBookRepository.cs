using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface;

public interface IBookRepository
{
    Task<List<Book>> GetBooks(RequestDTO input, bool? latest);
    Task<Book> GetBookById(int id);
    Task<Book> AddBook(BookDTO model);
    Task<Book> UpdateBook(int id, BookDTO model);
    Task<Book> DeleteBook(int id);
    Task<Book> ImportBook(int id, int quantity);
    Task<Book> SellBook(int id, int quantity);
}