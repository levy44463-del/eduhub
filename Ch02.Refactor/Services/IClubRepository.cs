using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public interface IClubRepository
{
    void Add(Club club);
    Club? GetById(Guid id);
    List<Club> GetAll();
    int CountMembers(Guid clubId);
    bool HasRoom(Guid clubId);
    void AddMember(Member member);
    List<Member> GetMembers(Guid clubId);
}
