using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.RequestCounter
{
    public class RequestSummaryItem
    {
        public RequestSummaryItem(string path, int counter)
        {
            Path = path;
            Counter = counter;
        }

        public string Path { get; set; }
        public int Counter { get; set; }
    }
}
