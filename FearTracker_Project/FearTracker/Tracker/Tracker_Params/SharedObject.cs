using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT
{
    /// <summary>
    /// Collection type to share data among multiple threads.
    /// </summary>
    public class SharedObject
    {
        public TrackerParams trackerParams { get; set; }
    }
}
