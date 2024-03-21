using BusinessObject.DTO;
using BusinessObject.Model;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository;

public class BookRepository : IBookRepository
{
    private readonly BookDAO _dao = BookDAO.Instance;
    public Task<List<Book>> GetBooks(RequestDTO input, bool? latest) => _dao.GetBooksAsync(input, latest);
    public Task<Book> GetBookById(int id) => _dao.GetBookByIdAsync(id);
    public Task<Book> AddBook(BookDTO model) => _dao.AddBookASync(model);
    public Task<Book> UpdateBook(int id, BookDTO model) => _dao.UpdateBookAsync(id, model);
    public Task<Book> DeleteBook(int id) => _dao.DeleteBookAsync(id);
    public Task<Book> ImportBook(int id, int quantity) => _dao.ImportBookAsync(id, quantity);
    public Task<Book> SellBook(int id, int quantity) => _dao.SellBookAsync(id, quantity);
}