namespace DataAccess.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException() : base("Password is incorrect")
        {
        }
    }
}
