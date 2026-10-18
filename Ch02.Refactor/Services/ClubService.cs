using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Services;

public class ClubService
{
    private readonly IClubRepository _clubRepository;
    private readonly IAppLogger _logger;
    private readonly INotificationServiceFactory _notificationFactory;

    public ClubService(
        IClubRepository clubRepository,
        IAppLogger logger,
        INotificationServiceFactory notificationFactory)
    {
        _clubRepository = clubRepository;
        _logger = logger;
        _notificationFactory = notificationFactory;
    }

    public Club CreateClub(string name, string description, string presidentEmail)
    {
        var club = Club.Create(name, description);
        _clubRepository.Add(club);

        _logger.Log($"Club created: {name}");

        var channel = ResolveChannel(presidentEmail);
        var notificationService = _notificationFactory.Create(channel);
        notificationService.Send(presidentEmail, $"Club '{name}' has been created.");

        return club;
    }

    public void AddMemberToClub(Guid clubId, string memberName, string email)
    {
        var club = _clubRepository.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        if (!_clubRepository.HasRoom(clubId))
            throw new InvalidOperationException("Club is full (max 50 members)");

        var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
        _clubRepository.AddMember(member);

        _logger.Log($"Member {memberName} joined {club.Name}");

        var notificationService = _notificationFactory.Create(NotificationChannel.Generic);
        notificationService.Send(email, $"Welcome to {club.Name}");

        var slackService = _notificationFactory.Create(NotificationChannel.Slack);
        slackService.Send(email, $"{memberName} joined {club.Name}");
    }

    private static NotificationChannel ResolveChannel(string email)
    {
        if (email.Contains("@gmail.com"))
            return NotificationChannel.Gmail;
        if (email.Contains("@yahoo.com"))
            return NotificationChannel.Yahoo;
        return NotificationChannel.Generic;
    }
}
