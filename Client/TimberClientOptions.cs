using System.Text.Json;
using TimberNet.Core.Logging;

namespace TimberNet.Core.Client;

public class TimberClientOptions
{
  public string BaseAddress { get; set; } = "http://localhost:8080/api/";
  public bool ThrowOnError { get; set; } = false;
  public TimeSpan? Timeout { get; set; }
  public HttpClient? HttpClient { get; set; }
  public JsonSerializerOptions? JsonSerializerOptions { get; set; }
  public ITimberLogger? Logger { get; set; }
}