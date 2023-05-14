using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameTracker
{
    internal class KeyboardEvent : TrackerEvent
    {
        int numInputs;
        public KeyboardEvent(CommonContent common) : base(common)
        {
            eventType_ = "2Keyboard";
        }

        public override string toCSV()
        {
            //Base information
            string format = base.toCSV();

            return format + "\n";
        }

        public override string toJSON()
        {
            //Base information
            string format = base.toJSON();

            //collection data
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(format);

            //Add data
            data["y"] = numInputs;

            // Serialize collection with new data
            string newCollection = JsonConvert.SerializeObject(data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            newCollection += ",\n";

            return newCollection;
        }

        public void setNumInputs(short ni)
        {
            numInputs = ni;
        }
    }
}