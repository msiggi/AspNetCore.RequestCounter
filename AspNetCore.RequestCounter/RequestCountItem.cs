using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.RequestCounter
{
    public class RequestCountItem
    {
        public RequestCountItem(string path, TimeSpan duration, string ip)
        {
            Path = path;
            Duration = duration;
            Ip = ip;
        }

        public string Path { get; set; }
        public string Ip { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
