﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.RequestCounter
{
    public static class RequestCounter
    {
        public static ConcurrentBag<RequestCountItem> RequestCountItems { get; set; } = new ConcurrentBag<RequestCountItem>();

        public static void AddRequestCount(string path, TimeSpan duration)
        {
            RequestCountItems.Add(new RequestCountItem(path, duration));
        }

        public static ConcurrentBag<RequestSummaryItem> RequestSummaryByPath
        {
            get
            {
                var grouped = RequestCountItems.GroupBy(x => x.Path).Select(g => new RequestSummaryItem(g.Key, g.Count()));

                return new ConcurrentBag<RequestSummaryItem>(grouped);
            }
        }
    }
}
