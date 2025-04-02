using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimePeriod
{
    class Time
    {
        private double seconds; 

        public double Hours
        {
            get { return seconds / 3600; }  
            set { seconds = (value >= 0) ? value * 3600 : 0; }
        }

        public Time(double hours)
        {
            Hours = hours; 
        }
    }
}
