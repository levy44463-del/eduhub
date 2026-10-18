using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class ConsoleReportStrategy : IReportStrategy
{
    public string Generate(List<Club> clubs, List<Member> members)
    {
        var lines = new List<string>();
        foreach (var club in clubs)
        {
            var memberCount = members.Count(m => m.ClubId == club.Id);
            lines.Add($"Club: {club.Name} | Members: {memberCount}");
        }
        return string.Join(Environment.NewLine, lines);
    }
}
