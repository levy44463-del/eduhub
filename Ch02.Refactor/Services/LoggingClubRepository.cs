using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class LoggingClubRepository : IClubRepository
{
    private readonly IClubRepository _inner;
    private readonly IAppLogger _logger;

    public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public void Add(Club club)
    {
        _inner.Add(club);
        _logger.Log($"Club created: {club.Name}");
    }

    public Club? GetById(Guid id)
    {
        var club = _inner.GetById(id);
        _logger.Log($"Get club by id: {id}");
        return club;
    }

    public List<Club> GetAll()
    {
        var clubs = _inner.GetAll();
        _logger.Log($"Get all clubs, count={clubs.Count}");
        return clubs;
    }

    public int CountMembers(Guid clubId)
    {
        var count = _inner.CountMembers(clubId);
        _logger.Log($"Count members for club {clubId}: {count}");
        return count;
    }

    public bool HasRoom(Guid clubId)
    {
        var hasRoom = _inner.HasRoom(clubId);
        _logger.Log($"Has room for club {clubId}: {hasRoom}");
        return hasRoom;
    }

    public void AddMember(Member member)
    {
        _inner.AddMember(member);
        _logger.Log($"Member added: {member.Name} to club {member.ClubId}");
    }

    public List<Member> GetMembers(Guid clubId)
    {
        var members = _inner.GetMembers(clubId);
        _logger.Log($"Get members for club {clubId}: {members.Count}");
        return members;
    }
}
