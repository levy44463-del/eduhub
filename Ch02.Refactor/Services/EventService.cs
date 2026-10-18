using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class EventService
{
    private readonly IClubRepository _clubRepository;
    private readonly IAppLogger _logger;
    private readonly List<IEventObserver> _observers = new();

    public EventService(IClubRepository clubRepository, IAppLogger logger)
    {
        _clubRepository = clubRepository;
        _logger = logger;
    }

    public void RegisterObserver(IEventObserver observer)
    {
        _observers.Add(observer);
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date)
    {
        var club = _clubRepository.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        var evt = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        _logger.Log($"Event created: {title}");

        var members = _clubRepository.GetMembers(clubId);
        foreach (var observer in _observers)
        {
            observer.OnEventCreated(evt, club, members);
        }

        return evt;
    }
}
