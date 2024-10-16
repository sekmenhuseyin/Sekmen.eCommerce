namespace Sekmen.Commerce.Services.Auth.Application.Models;

public record ResponseDto<T>
{
    public T? Result { get; protected set; }
    public bool IsSuccess { get; protected set; }
    public string? ErrorMessage { get; protected set; }

    public static ResponseDto<T> Success(T result)
    {
        return new ResponseDto<T>
        {
            IsSuccess = true,
            Result = result
        };
    }

    public static ResponseDto<T> Error(string message)
    {
        return new ResponseDto<T>
        {
            IsSuccess = false,
            ErrorMessage = message
        };
    }

    public static ResponseDto<T> NotFound()
    {
        return new ResponseDto<T>
        {
            IsSuccess = false,
            ErrorMessage = nameof(NotFound)
        };
    }
}