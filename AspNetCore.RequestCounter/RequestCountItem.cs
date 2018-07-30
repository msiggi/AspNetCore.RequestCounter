using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.RequestCounter
{
    public class RequestCountItem
    {
        public RequestCountItem(string path, TimeSpan duration)
        {
            Path = path;
            Duration = duration;
        }

        public string Path { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
