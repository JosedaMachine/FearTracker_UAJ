using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioTracking;

namespace FT
{
    public class TrackerParams
    {
        public bool mouseTracking { get; set; }
        public bool MicTracking { get; set; }
        public bool KeyboardTracking { get; set; }

        public AudioTracker audioTracker { get; set; }
        
        public int trackingCount { get; set; } = 0;

        public Process process { get; set; }
        public bool canStart { get; set; }
        public bool canStop { get; set; }

        public long startTime { get; set; }
    }
}
