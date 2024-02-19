namespace Invention.Exceptions;

public class CustomException : Exception
{
    public int StatusCode { get; }
    public string Message { get; }
    public CustomException(string message)
    {
        this.StatusCode = 400;
        this.Message = message;
    }
}