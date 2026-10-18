using EduHub.Ch02.Domain;
using EduHub.Ch02.Services;

var logger = new ConsoleAppLogger();
var repository = new LoggingClubRepository(new ClubRepository(), logger);
var notificationFactory = new NotificationServiceFactory();

var clubService = new ClubService(repository, logger, notificationFactory);
var eventService = new EventService(repository, logger);
eventService.RegisterObserver(new EmailNotifier());
eventService.RegisterObserver(new LogNotifier());
var reportService = new ReportService(repository, logger);

var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
clubService.AddMemberToClub(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");
eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

var report = reportService.Generate(new ConsoleReportStrategy());
Console.WriteLine(report);

var csvReport = reportService.Generate(new CsvReportStrategy());
Console.WriteLine(csvReport);
