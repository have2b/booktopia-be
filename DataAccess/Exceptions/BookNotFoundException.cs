namespace DataAccess.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(int id) : base($"Book with id {id} not found")
    {
    }
}