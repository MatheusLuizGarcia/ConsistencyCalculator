using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Models
{
    public class TagStatistics
    {
        public string Tag { get; }
        public int Successes { get; private set; }
        public int Total { get; private set; }

        public double Percentage =>
            Total == 0 ? 0 : (double)Successes / Total * 100;

        public TagStatistics(string tag)
        {
            Tag = tag;
        }

        public void Register(bool success)
        {
            Total++;
            if (success)
                Successes++;
        }
    }
}
