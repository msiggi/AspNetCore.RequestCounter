using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.RequestCounter
{
    public class RequestCounterMiddleware
    {

        readonly RequestDelegate _next;
        private List<string> _excludedPathes;

        public RequestCounterMiddleware(RequestDelegate next, List<string> excludedPathes)
        {
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
                sw.Stop();

                if (!_excludedPathes.Any(x => httpContext.Request.Path.ToString().Contains(x)))
                {
                    RequestCounter.AddRequestCount(httpContext.Request.Path, sw.Elapsed);
                }

            }
            catch
            {

            }

        }

    }
}
