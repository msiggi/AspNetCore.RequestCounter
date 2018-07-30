# AspNetCore.RequestCounter
A Middleware for AspNet-Core-Projects using for Server-Side Request-Logging, providing some basic summaries.

## Usage
(in Startup.cs --> public void Configure..)

```csharp

  app.UseRequestCounterMiddleware(new List<string>());

```

also available as Nuget-Package: Install-Package AspNetCore.RequestCounter

