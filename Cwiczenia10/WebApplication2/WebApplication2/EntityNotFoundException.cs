namespace WebApplication2.Exception;

public class EntityNotFoundException : System.Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
    
    public EntityNotFoundException()
    {
    }
}