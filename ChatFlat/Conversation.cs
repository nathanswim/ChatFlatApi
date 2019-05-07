using Newtonsoft.Json;
using System.Collections.Generic;

namespace Playground
{
    public class Conversation : FlatChatObject
    {
        [JsonProperty("messages")]
        public IList<Message> Messages { get; set; }
    }
}
