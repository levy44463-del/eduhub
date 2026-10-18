namespace EduHub.Ch02.Services;

public class SmsNotificationService : INotificationService
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Sending SMS to {recipient}: {message}");
    }
}
