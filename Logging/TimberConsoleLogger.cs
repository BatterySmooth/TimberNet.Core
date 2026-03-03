namespace TimberNet.Core.Logging;

internal sealed class TimberConsoleLogger : ITimberLogger
{
  public void LogInfo(string message)
  {
    Console.WriteLine($"[Timber  INFO] {message}");
  }

  public void LogError(string message, Exception? exception = null)
  {
    Console.WriteLine($"[Timber ERROR] {message}");
    if (exception != null)
      Console.WriteLine(exception);
  }
}