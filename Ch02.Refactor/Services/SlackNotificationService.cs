namespace EduHub.Ch02.Services;

public class SlackNotificationService : INotificationService
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Sending Slack notification to {recipient}: {message}");
    }
}
