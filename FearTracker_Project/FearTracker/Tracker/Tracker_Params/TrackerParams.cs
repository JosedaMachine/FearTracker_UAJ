using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioTracking;

namespace FT
{
    /// <summary>
    /// Collection to store data which will use multiple threads along the project.
    /// </summary>
    public class TrackerParams
    {
        public bool mouseTracking { get; set; }
        public bool MicTracking { get; set; }
        public bool KeyboardTracking { get; set; }

        public int trackingCount { get; set; } = 1; // Conteo de herramientas de seguimiento activas.

        public Process process { get; set; }
        public bool canStart { get; set; }
        public bool canStop { get; set; }

        public long startTime { get; set; } // Tiempo de inicio del tracker

        public long recordingTimeMilliseconds { get; set; } = 500; // Intervalo de tiempo en el que se recogen los eventos
    }
}
