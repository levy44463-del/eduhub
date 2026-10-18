using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class EmailNotificationService : INotificationService
{
    private readonly string _provider;

    public EmailNotificationService(string provider)
    {
        _provider = provider;
    }

    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Sending {_provider} email to {recipient}: {message}");
    }
}
