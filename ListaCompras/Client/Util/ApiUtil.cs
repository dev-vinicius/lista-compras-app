using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ListaCompras.Client.Util
{
    public static class ApiUtil
    {
        public static HttpContent ObjectToHttpContent(object obj)
        {
            var objAsJson = JsonSerializer.Serialize(obj);
            return new StringContent(objAsJson,
                                     Encoding.UTF8,
                                     "application/json");
        }

        public static async Task<T> HttpContentToObject<T>(HttpContent content)
        {
            var contentAsStream = await content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentAsStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public static async Task<dynamic> HttpContentToObject(HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();
            return (dynamic)Newtonsoft.Json.Linq.JObject.Parse(contentAsString);
        }
    }
}
