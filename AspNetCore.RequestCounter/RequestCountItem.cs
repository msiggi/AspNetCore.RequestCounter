using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.RequestCounter
{
    public class RequestCountItem
    {
        public RequestCountItem(string path, TimeSpan duration, string ip, bool isMobile)
        {
            Path = path;
            Duration = duration;
            Ip = ip;
            IsMobile = isMobile;
        }

        public string Path { get; set; }
        public string Ip { get; set; }

        public bool IsMobile { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
