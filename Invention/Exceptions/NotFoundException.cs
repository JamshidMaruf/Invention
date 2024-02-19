using Invention.Models;
using System.Reflection;

namespace Invention.Exceptions;

#pragma warning disable
public class NotFoundException<T> : Exception
{
    public int StatusCode { get; }
    public string Message { get; }
    public NotFoundException()
    {
        this.StatusCode = 404;
        this.Message = $"{typeof(T)} is not found";
    }
}