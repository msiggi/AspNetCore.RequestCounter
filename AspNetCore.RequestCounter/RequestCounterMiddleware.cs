using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.RequestCounter
{
    public class RequestCounterMiddleware
    {

        readonly RequestDelegate _next;
        private List<string> _excludedPathes;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="excludedPathes">excludedPathes - Pathes not to count</param>
        public RequestCounterMiddleware(RequestDelegate next, List<string> excludedPathes)
        {
            RequestCounter.RequestCountStartDate = DateTime.UtcNow;

            _excludedPathes = excludedPathes;

            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
        }



        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var sw = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);
            }
            finally
            {
                sw.Stop();
                if (!_excludedPathes.Any(x => httpContext.Request.Path.ToString().Contains(x)))
                {
                    var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                    RequestCounter.AddRequestCount(httpContext.Request.Path, sw.Elapsed, ip, Utils.CheckIfMobile(httpContext));
                }
            }
        }


    }
}
