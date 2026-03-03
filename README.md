# TimberNet.Core

This mini SDK provides easy ways to work with the Timberborn web API to interact with Levers and Adapters.

## Usage

To get started, create the client, either with default or specified options. Generally, you'll want the default options.

```csharp
// Default client options
TimberClient _timberClient = new();

// Custom options
TimberClient _timberClient = new(new TimberClientOptions
{
    ThrowOnError = true,
    Timeout = new TimeSpan(...),
    JsonSerializerOptions = ...,
    Logger = new ITimberLogger(...),
});

// Adapting ITimberLogger to Microsoft's ILogger
TimberClient _timberClient = new(new TimberClientOptions
{
    Logger = new TimberLoggerAdapter(logger)
});
```

From that point, you have the following options for interactive with the API with GET and POST requests:

```csharp
_timberClient.Levers.GetAllAsync();
_timberClient.Levers.GetAsync("name");
_timberClient.Levers.SetColorAsync("name", "00ff00");
_timberClient.Levers.SwitchOnAsync("name");
_timberClient.Levers.SwitchOffAsync("name");
_timberClient.Levers.SetStateAsync("name", true);

_timberClient.Adapters.GetAllAsync();
_timberClient.Adapters.GetAsync("name");
```
