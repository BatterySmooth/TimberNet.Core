namespace TimberNet.Core.Logging;

public interface ITimberLogger
{
  void LogInfo(string message);
  void LogError(string message, Exception? exception = null);
}