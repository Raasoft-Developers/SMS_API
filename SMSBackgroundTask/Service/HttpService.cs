using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SMSBackgroundTask.Service
{
    public interface IHttpService
    {
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers);
    }

    public class HttpService : IHttpService
    {
        public HttpClient Client { get; set; }

        public HttpService()
        {
            Client = new HttpClient();
        }
        public HttpResponseMessage Get(string url)
        {
            return Client.GetAsync(url).Result;
        }

        public async Task<HttpResponseMessage> PostData(string url, object model)
        {
            string stringData = JsonConvert.SerializeObject(model);
            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
            return await Client.PostAsync(url, contentData);
        }

        public async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers)
        {
            var encodedContent = new FormUrlEncodedContent(parameters);
            //Client.BaseAddress = new Uri(url);
            foreach (var item in headers)
            {
                Client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
            return await Client.PostAsync(url, encodedContent);
        }
    }
}
