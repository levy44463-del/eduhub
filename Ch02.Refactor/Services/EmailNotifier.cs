using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class EmailNotifier : IEventObserver
{
    public void OnEventCreated(Event evt, Club club, List<Member> members)
    {
        foreach (var m in members)
        {
            Console.WriteLine($"Email to {m.Email}: new event '{evt.Title}' at {club.Name}");
        }
    }
}
