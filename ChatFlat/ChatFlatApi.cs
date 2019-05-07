using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public class ChatFlatApi
    {
        readonly string baseUri;
        readonly HttpClient client;
        readonly RestApi api;

        public ChatFlatApi()
        {
            this.baseUri = Settings.BaseUrl;

            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client = client;
            this.api = new RestApi(client);

        }

        public async Task<IEnumerable<Conversation>> GetConversations()
        {
            string query = $@"{baseUri}conversations";

            JArray array = await api.GetArray(query);
            return array.ToObject<IEnumerable<Conversation>>();
        }

        public async Task<Conversation> CreateConversation()
        {
            var query = $"{baseUri}conversations";
            var json = await api.PostObject(query, "");
            return json.ToObject<Conversation>();
        }

        public async Task<Conversation> GetConversation(string id)
        {
            string query = $@"{baseUri}conversations/{id}";
            var json = await api.GetObject(query);
            return json.ToObject<Conversation>();
        }

        public async Task<IEnumerable<Message>> GetConversationMessages(string id)
        {
            string query = $@"{baseUri}conversations/{id}/messages";
            var json = await api.GetArray(query);
            return json.ToObject<IEnumerable<Message>>();
        }

        public async Task<Message> CreateMessage(string conversationId, Message message)
        {
            string query = $@"{baseUri}conversations/{conversationId}/messages";
            var content = SerializeJson(message);
            var json = await api.PostObject(query, content.ToString(Formatting.None));
            return json.ToObject<Message>();
        }

        public async Task<bool> Reset()
        {
            string query = $@"{baseUri}conversations/reset";
            var response = await api.Get(query);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Reset(string conversationId)
        {
            string query = $@"{baseUri}conversations/{conversationId}/reset";
            var response = await api.Get(query);
            return response.IsSuccessStatusCode;
        }

        private static JObject SerializeJson(Message message)
        {
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            };

            return JObject.FromObject(message, serializer);
        }

    }
}
