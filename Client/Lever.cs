namespace TimberNet.Core.Client;

public sealed class Lever
{
  public string Name { get; init; } = default!;
  public bool State { get; init; }
  public bool SpringReturn { get; init; }
}