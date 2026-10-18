namespace EduHub.Ch02.Services;

public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel)
    {
        return channel switch
        {
            NotificationChannel.Gmail => new EmailNotificationService("Gmail"),
            NotificationChannel.Yahoo => new EmailNotificationService("Yahoo"),
            NotificationChannel.Generic => new EmailNotificationService("Generic"),
            NotificationChannel.Sms => new SmsNotificationService(),
            NotificationChannel.Slack => new SlackNotificationService(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
        };
    }
}
