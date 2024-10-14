namespace Sekmen.Commerce.WebApplication.Models;

public record ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

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