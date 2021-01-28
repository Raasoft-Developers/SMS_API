using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nvg.SMSBackgroundTask.Service
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
