namespace DataAccess.Exceptions;

public class PublisherNotFoundException : Exception
{
    public PublisherNotFoundException(int id) : base($"Publisher with id {id} not found")
    {
    }
}