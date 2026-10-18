using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public interface IReportStrategy
{
    string Generate(List<Club> clubs, List<Member> members);
}
