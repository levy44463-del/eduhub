using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public interface IEventObserver
{
    void OnEventCreated(Event evt, Club club, List<Member> members);
}
