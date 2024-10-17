namespace Sekmen.Commerce.Frontend.Application.Models.Auth;

public record LoginCommand(string UserName, string Password);
public record LoginResponseViewModel(UserDto User, string Token);
