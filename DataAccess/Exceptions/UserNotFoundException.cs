namespace DataAccess.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string userName) : base($"User {userName} not found")
    {
    }
}