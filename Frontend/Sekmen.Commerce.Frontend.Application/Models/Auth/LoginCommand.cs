namespace Sekmen.Commerce.Frontend.Application.Models.Auth;

public record LoginCommand
{
    [Required] public string UserName { get; init; }
    [Required] public string Password { get; init; }
}

public record LoginResponseViewModel(UserDto User, string Token);
