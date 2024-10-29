namespace Sekmen.Commerce.Email.Infrastructure;

public interface IEmailService
{
    Task<bool> SendAsync(string recipient, string subject, string body);
}

public sealed class EmailService : IEmailService
{
    public Task<bool> SendAsync(string recipient, string subject, string body)
    {
        Console.WriteLine("EmailService.SendAsync: " + recipient + subject);
        return Task.FromResult(true);
    }
}