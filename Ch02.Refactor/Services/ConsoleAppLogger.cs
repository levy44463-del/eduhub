namespace EduHub.Ch02.Services;

public class ConsoleAppLogger : IAppLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}");
    }
}
