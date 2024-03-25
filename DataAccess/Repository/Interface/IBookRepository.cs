using BusinessObject.DTO;
using BusinessObject.Model;

namespace DataAccess.Repository.Interface;

public interface IBookRepository
{
    Task<ListBooksResponseDTO> GetBooks(SearchBookDTO input, bool? latest);
    Task<List<Book>> SearchBooks(SearchBookDTO model);
    Task<Book> GetBookById(int id);
    Task<Book> AddBook(BookDTO model);
    Task<Book> UpdateBook(int id, BookDTO model);
    Task<Book> DeleteBook(int id);
    Task<Book> ImportBook(int id, int quantity);
    Task<Book> SellBook(int id, int quantity);
    Task<int> GetBookCount();
}