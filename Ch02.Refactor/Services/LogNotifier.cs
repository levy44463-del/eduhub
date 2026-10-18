using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class LogNotifier : IEventObserver
{
    public void OnEventCreated(Event evt, Club club, List<Member> members)
    {
        Console.WriteLine($"[LOG] Event created: {evt.Title} at {club.Name}, {members.Count} members notified");
    }
}
