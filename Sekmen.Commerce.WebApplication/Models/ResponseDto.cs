namespace Sekmen.Commerce.WebApplication.Models;

public record ResponseDto
{
    public object? Result { get; protected set; }
    public bool IsSuccess { get; protected set; }
    public string? ErrorMessage { get; protected set; }

    public ResponseDto Success(object result)
    {
        IsSuccess = true;
        Result = result;

        return this;
    }

    public ResponseDto Error(string message)
    {
        IsSuccess = false;
        ErrorMessage = message;

        return this;
    }

    public ResponseDto NotFound()
    {
        IsSuccess = false;
        ErrorMessage = nameof(NotFound);

        return this;
    }
}