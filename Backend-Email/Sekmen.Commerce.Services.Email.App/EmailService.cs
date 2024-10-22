namespace Sekmen.Commerce.Services.Email.App;

public interface IEmailService
{
    Task<bool> SendAsync(string recipient, string subject, string body);
}

internal sealed class EmailService : IEmailService
{
    public Task<bool> SendAsync(string recipient, string subject, string body)
    {
        throw new NotImplementedException();
    }
}