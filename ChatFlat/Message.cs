using Newtonsoft.Json;

namespace Playground
{
    public class Message : FlatChatObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
