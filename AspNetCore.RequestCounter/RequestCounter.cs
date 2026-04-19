using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace AspNetCore.RequestCounter
{
    public static class RequestCounter
    {
        private class PathSummary
        {
            public int Total;
            public int Mobile;
        }

        private static readonly ConcurrentDictionary<string, PathSummary> _summaryCache = new();

        public static DateTime RequestCountStartDate { get; set; }
        public static ConcurrentBag<RequestCountItem> RequestCountItems { get; set; } = new ConcurrentBag<RequestCountItem>();

        public static void AddRequestCount(string path, TimeSpan duration, string ip, bool isMobile)
        {
            RequestCountItems.Add(new RequestCountItem(path, duration, ip, isMobile));

            var summary = _summaryCache.GetOrAdd(path, _ => new PathSummary());
            Interlocked.Increment(ref summary.Total);
            if (isMobile) Interlocked.Increment(ref summary.Mobile);
        }

        public static ConcurrentBag<RequestSummaryItem> RequestSummaryByPath =>
            new ConcurrentBag<RequestSummaryItem>(
                _summaryCache.Select(kvp => new RequestSummaryItem(kvp.Key, kvp.Value.Total, kvp.Value.Mobile))
            );
    }
}
