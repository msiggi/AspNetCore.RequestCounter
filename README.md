# AspNetCore.RequestCounter
A Middleware for AspNet-Core-Projects using for Server-Side Request-Logging, providing some basic summaries.

## Usage
(in Startup.cs --> public void Configure, as a parameter its possible to define exceptions)

```csharp
  app.UseRequestCounterMiddleware(new List<string>{ "__browserLink", "robots", "serviceworker", "_blazor", "css", "js", "signalr" });
```

also available as Nuget-Package: Install-Package AspNetCore.RequestCounter


After installing and configuring you have access to 

```csharp
  AspNetCore.RequestCounter.RequestCounter.RequestSummaryByPath
  AspNetCore.RequestCounter.RequestCounter.RequestCountItems
 
  // more coming soon
  
```
