namespace DataAccess.Exceptions;

public class InsufficientQuantityException : Exception
{
    public InsufficientQuantityException(string bookName) : base($"{bookName} is in insufficient quantity")
    {
    }
}