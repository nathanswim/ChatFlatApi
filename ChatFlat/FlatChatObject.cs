using Newtonsoft.Json;
using System;

namespace Playground
{
    public abstract class FlatChatObject
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
