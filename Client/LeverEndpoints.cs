namespace TimberNet.Core.Client;

public sealed class LeverEndpoints
{
  private readonly TimberClient _client;

  internal LeverEndpoints(TimberClient client)
  {
    _client = client;
  }

  public Task<List<Lever>?> GetAllAsync() =>
    _client.GetAsync<List<Lever>>("levers");

  public Task<Lever?> GetAsync(string name) =>
    _client.GetAsync<Lever>($"levers/{Uri.EscapeDataString(name)}");

  public Task SetColorAsync(string name, string hexColor) =>
    _client.PostAsync($"color/{Uri.EscapeDataString(name)}/{hexColor}");

  public Task SwitchOnAsync(string name) =>
    _client.PostAsync($"switch-on/{Uri.EscapeDataString(name)}");

  public Task SwitchOffAsync(string name) =>
    _client.PostAsync($"switch-off/{Uri.EscapeDataString(name)}");

  public Task SetStateAsync(string name, bool state) =>
    state ? SwitchOnAsync(name) : SwitchOffAsync(name);
}