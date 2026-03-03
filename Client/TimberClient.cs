using System.Net.Http.Json;
using TimberNet.Core.Logging;

namespace TimberNet.Core.Client;

public sealed class TimberClient
{
  private readonly HttpClient _client;
  private readonly TimberClientOptions _options;
  private readonly ITimberLogger _logger;
  public LeverEndpoints Levers { get; }
  public AdapterEndpoints Adapters { get; }
  
  public TimberClient(TimberClientOptions? options = null)
  {
    _options = options ?? new TimberClientOptions();
    if (_options.HttpClient != null)
    {
      _client = _options.HttpClient;
    }
    else
    {
      if (!Uri.TryCreate(_options.BaseAddress, UriKind.Absolute, out var uri))
        throw new ArgumentException($"Invalid BaseAddress in {nameof(TimberClientOptions)}");
      _client = new HttpClient { BaseAddress = uri };
    }
    if (_options.Timeout.HasValue)
      _client.Timeout = _options.Timeout.Value;
    _logger = _options.Logger ?? new TimberConsoleLogger();
    Levers = new LeverEndpoints(this);
    Adapters = new AdapterEndpoints(this);
  }
  
  #region HTTP Calls
  
  internal async Task<T?> GetAsync<T>(string endpoint)
  {
    try
    {
      var response = await _client.GetAsync(endpoint);
      if (!response.IsSuccessStatusCode)
        return HandleFailure<T>("GET", endpoint, response);
      return await response.Content.ReadFromJsonAsync<T>(_options.JsonSerializerOptions);
    }
    catch (Exception ex)
    {
      return HandleException<T>("GET", endpoint, ex);
    }
  }
  
  internal async Task PostAsync(string endpoint)
  {
    try
    {
      var response = await _client.PostAsync(endpoint, null);
      if (!response.IsSuccessStatusCode)
        HandleFailure<object>("POST", endpoint, response);
    }
    catch (Exception ex)
    {
      HandleException<object>("POST", endpoint, ex);
    }
  }
  
  #endregion
  
  #region Error Handling
  
  private T? HandleFailure<T>(string method, string endpoint, HttpResponseMessage response)
  {
    var message = $"{method} {endpoint} failed: {(int)response.StatusCode} {response.StatusCode}";
    _logger.LogError(message);
    return _options.ThrowOnError
      ? throw new HttpRequestException(message)
      : default;
  }

  private T? HandleException<T>(string method, string endpoint, Exception ex)
  {
    var message = $"{method} {endpoint} threw exception: {ex.Message}";
    _logger.LogError(message);
    return _options.ThrowOnError
      ? throw new Exception(message, ex)
      : default;
  }
  
  #endregion
}