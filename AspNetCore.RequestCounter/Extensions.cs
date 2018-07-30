using AspNetCore.RequestCounter;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

public static class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="excludedPathes">Part of the pathes, which should not logged.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseRequestCounterMiddleware(this IApplicationBuilder app, List<string> excludedPathes)
    {
        return app.UseMiddleware<RequestCounterMiddleware>(excludedPathes);
    }
    
}