# AspNetCore.RequestCounter
A Middleware for AspNet-Core-Projects using for Server-Side Request-Logging, providing some basic summaries.

## Usage
(in Startup.cs --> public void Configure..)

```csharp

  app.UseRequestCounterMiddleware(new List<string>());

```

also available as Nuget-Package: Install-Package AspNetCore.RequestCounter


After installing and configuring you have access to 

```csharp

  AspNetCore.RequestCounter.RequestCounter.RequestSummaryByPath
  AspNetCore.RequestCounter.RequestCounter.RequestCountItems
  
  // more coming soon
  
```
