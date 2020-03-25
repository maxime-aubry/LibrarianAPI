using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.RestFulAPI.Tests.Tools
{
    public static class HttpHelper
    {
        public static async Task<ContentResult<TResult>> Post<TModel, TResult>(HttpClient client, string url, TModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response = await client.PostAsync(url, formContent).Result.Content.ReadAsStringAsync();
            ContentResult<TResult> result = JsonConvert.DeserializeObject<ContentResult<TResult>>(response);
            return result;
        }

        public static async Task<ContentResult<TResult>> Put<TModel, TResult>(HttpClient client, string url, TModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent formContent = new StringContent(json, Encoding.UTF8, "application/json");
            string response = await client.PutAsync(url, formContent).Result.Content.ReadAsStringAsync();
            ContentResult<TResult> result = JsonConvert.DeserializeObject<ContentResult<TResult>>(response);
            return result;
        }

        public static async Task<ContentResult<TResult>> Delete<TResult>(HttpClient client, string url)
        {
            string response = await client.DeleteAsync(url).Result.Content.ReadAsStringAsync();
            ContentResult<TResult> result = JsonConvert.DeserializeObject<ContentResult<TResult>>(response);
            return result;
        }

        public static async Task<ContentResult<TResult>> Get<TResult>(HttpClient client, string url, List<KeyValuePair<string, string>> dictionary = null)
        {
            if (dictionary != null && dictionary.Any())
            {
                string query = new FormUrlEncodedContent(dictionary).ReadAsStringAsync().Result;
                url = $"{url}?{query}";
            }
            string response = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            ContentResult<TResult> result = JsonConvert.DeserializeObject<ContentResult<TResult>>(response);
            return result;
        }
    }
}
