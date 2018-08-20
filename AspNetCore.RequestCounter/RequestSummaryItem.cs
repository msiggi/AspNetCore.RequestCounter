using System;

namespace AspNetCore.RequestCounter
{
    public class RequestSummaryItem
    {
        public RequestSummaryItem(string path, int counter, int mobileCount)
        {
            Path = path;
            Counter = counter;

            double perc = (mobileCount * 100 / counter);
            MobileQuota = (int)Math.Round(perc, 0);
        }

        public string Path { get; set; }
        public int Counter { get; set; }
        public int MobileQuota { get; set; }

    }
}