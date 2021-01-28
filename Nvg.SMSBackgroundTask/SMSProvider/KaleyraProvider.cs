using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nvg.SMSBackgroundTask.Service;

namespace Nvg.SMSBackgroundTask.SMSProvider
{
    public class KaleyraProvider : ISMSProvider
    {
        private readonly SMSProviderConnectionString _smsProviderCS;

        public KaleyraProvider(SMSProviderConnectionString smsProviderConnectionString)
        {
            _smsProviderCS = smsProviderConnectionString;
        }

        public async Task<string> SendSMS(string recipients, string message, string sender = "")
        {
            if (string.IsNullOrEmpty(sender))
                sender = _smsProviderCS.Sender;

            var url = _smsProviderCS.ApiUrl;
            var parameters = new Dictionary<string, string> {
                { "sender", sender },
                { "to", recipients },
                { "body", message }
            };
            var headers = new Dictionary<string, string>() {
                    { "api-key", _smsProviderCS.Fields["ApiKey"] }
                };
            HttpService httpService = new HttpService();
            HttpResponseMessage response = await httpService.Post(url,
                parameters,
                headers);
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            return apiResponse;
        }
    }
}
