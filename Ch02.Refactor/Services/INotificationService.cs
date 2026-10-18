using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public interface INotificationService
{
    void Send(string recipient, string message);
}

public enum NotificationChannel
{
    Gmail,
    Yahoo,
    Generic,
    Sms,
    Slack
}
