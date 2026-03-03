using Microsoft.Extensions.Logging;

namespace TimberNet.Core.Logging;

public class TimberLoggerAdapter(ILogger logger) : ITimberLogger
{
  public void LogInfo(string message)
  {
    logger.LogInformation(message);
  }

  public void LogError(string message, Exception? exception = null)
  {
    logger.LogError(exception, message);
  }
}