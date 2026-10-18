using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class ReportService
{
    private readonly IClubRepository _clubRepository;
    private readonly IAppLogger _logger;

    public ReportService(IClubRepository clubRepository, IAppLogger logger)
    {
        _clubRepository = clubRepository;
        _logger = logger;
    }

    public string Generate(IReportStrategy strategy)
    {
        var clubs = _clubRepository.GetAll();
        var members = clubs.SelectMany(c => _clubRepository.GetMembers(c.Id)).ToList();
        var report = strategy.Generate(clubs, members);
        _logger.Log("Report generated");
        return report;
    }
}
