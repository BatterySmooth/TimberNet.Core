namespace TimberNet.Core.Client;

public sealed class AdapterEndpoints
{
  private readonly TimberClient _client;

  internal AdapterEndpoints(TimberClient client)
  {
    _client = client;
  }

  public Task<List<Adapter>?> GetAllAsync() =>
    _client.GetAsync<List<Adapter>>("adapters");

  public Task<Adapter?> GetAsync(string name) =>
    _client.GetAsync<Adapter>($"adapters/{Uri.EscapeDataString(name)}");
}