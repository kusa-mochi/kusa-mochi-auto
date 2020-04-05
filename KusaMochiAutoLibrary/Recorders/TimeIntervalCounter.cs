using System;
using System.Collections.Generic;
using System.Text;

using KusaMochiAutoLibrary.NativeFunctions;

namespace KusaMochiAutoLibrary.Recorders
{
    internal class TimeIntervalCounter
    {
        internal TimeIntervalCounter()
        {
            NativeMethods.QueryPerformanceFrequency(ref _frequency);
        }

        /// <summary>
        /// current counter value [msec]
        /// </summary>
        internal double CurrentCount
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

        internal void Start()
        {
            NativeMethods.QueryPerformanceCounter(ref _startCounter);
        }

        internal void Restart()
        {
            Start();
        }
    }
}
