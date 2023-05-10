using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameTracker
{
    internal class MouseScareEvent : TrackerEvent
    {
        float mouseVelocity;
        public MouseScareEvent(CommonContent common) : base(common)
        {
            eventType_ = "MouseScare";
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
            data["y"] = mouseVelocity;

            // Serialize collection with new data
            string newCollection = JsonConvert.SerializeObject(data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            newCollection += ",\n";

            return newCollection;
        }

        public void setMouseVelocity(float mv)
        {
            mouseVelocity = mv;
        }
    }
}