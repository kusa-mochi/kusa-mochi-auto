using System;
using System.Collections.Generic;
using System.Text;

namespace KusaMochiAutoLibrary
{
    public class TimeIntervalCounter
    {
        public TimeIntervalCounter()
        {
            NativeMethods.QueryPerformanceFrequency(ref _frequency);
        }

        /// <summary>
        /// current counter value [msec]
        /// </summary>
        public double CurrentCount
        {
            get
            {
                NativeMethods.QueryPerformanceCounter(ref _stopCounter);
                return (double)(_stopCounter - _startCounter) * 1000.0 / _frequency;
            }
        }

        private long _startCounter = 0L;
        private long _stopCounter = 0L;
        private long _frequency = 0L;

        public void Start()
        {
            NativeMethods.QueryPerformanceCounter(ref _startCounter);
        }

        public void Restart()
        {
            Start();
        }
    }
}
