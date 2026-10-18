using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class CsvReportStrategy : IReportStrategy
{
    public string Generate(List<Club> clubs, List<Member> members)
    {
        var lines = new List<string> { "Club Name,Member Count" };
        foreach (var club in clubs)
        {
            var memberCount = members.Count(m => m.ClubId == club.Id);
            lines.Add($"{club.Name},{memberCount}");
        }
        return string.Join(Environment.NewLine, lines);
    }
}
