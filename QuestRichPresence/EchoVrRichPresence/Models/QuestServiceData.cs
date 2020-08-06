using Newtonsoft.Json;

namespace EchoVrRichPresence.Models
{
    public class QuestServiceData
    {
        [JsonProperty(PropertyName = "ownAdress")]
        public string QuestIp { get; set; }

        [JsonProperty(PropertyName = "pcIp")]
        public string PcIp { get; set; }

        [JsonProperty(PropertyName = "currentTop")]
        public string AppName { get; set; }

        [JsonProperty(PropertyName = "batteryLevel")]
        public string BatteryLevel { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}