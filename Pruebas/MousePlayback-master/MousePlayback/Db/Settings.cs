//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MousePlayback.Db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Settings
    {
        public long Id { get; set; }
        public long RepeatTimes { get; set; }
        public long RepeatForever { get; set; }
        public long RandomizeInput { get; set; }
        public string HotKeyStartStopRecording { get; set; }
        public string HotKeyPlaybackRecording { get; set; }
        public Nullable<long> RndSleepTime { get; set; }
        public Nullable<long> RndPixels { get; set; }
    }
}