using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class ClubRepository : IClubRepository
{
    private readonly List<Club> _clubs = new();
    private readonly List<Member> _members = new();

    public void Add(Club club)
    {
        _clubs.Add(club);
    }

    public Club? GetById(Guid id)
    {
        return _clubs.FirstOrDefault(c => c.Id == id);
    }

    public List<Club> GetAll()
    {
        return _clubs;
    }

    public int CountMembers(Guid clubId)
    {
        return _members.Count(m => m.ClubId == clubId);
    }

    public bool HasRoom(Guid clubId)
    {
        var club = GetById(clubId);
        if (club is null)
            return false;
        return club.HasRoom(CountMembers(clubId));
    }

    public void AddMember(Member member)
    {
        _members.Add(member);
    }

    public List<Member> GetMembers(Guid clubId)
    {
        return _members.Where(m => m.ClubId == clubId).ToList();
    }
}
