using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public class RestApi
    {
        readonly HttpClient client;

        public RestApi(HttpClient client)
        {
            this.client = client;
        }

        public async Task<JObject> GetObject(string query)
        {
            var json = await GetString(query);
            return JObject.Parse(json);
        }

        public async Task<JArray> GetArray(string query)
        {
            var json = await GetString(query);
            return JArray.Parse(json);
        }

        public async Task<string> GetString(string query)
        {
            var response = await Get(query);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> Get(string query)
        {
            return await client.GetAsync(query);
        }


        public async Task<JObject> PostObject(string query, string contentText)
        {
            var json = await PostString(query, contentText);
            return JObject.Parse(json);
        }

        public async Task<string> PostString(string query, string contentText)
        {
            var response = await Post(query, contentText);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> Post(string query, string content)
        {
            HttpContent c = new StringContent(content, Encoding.UTF8, Settings.JsonMediaType);
            return await client.PostAsync(query, c);
        }

    }
}
