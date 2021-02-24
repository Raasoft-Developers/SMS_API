using Newtonsoft.Json.Linq;
using Nvg.SMSBackgroundTask.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nvg.SMSBackgroundTask.SMSProvider
{
    public class VariforrmProvider : ISMSProvider
    {
        private readonly SMSProviderConnectionString _smsProviderCS;

        public VariforrmProvider(SMSProviderConnectionString smsProviderConnectionString)
        {
            _smsProviderCS = smsProviderConnectionString;
        }

        public async Task<string> SendSMS(string recipients, string message, string sender = null)
        {
            string responseMsg = "NOT SENT";
            string method = string.Empty;

            var url = _smsProviderCS.Fields["url"];
            var apiKey = _smsProviderCS.Fields["key"];

            if (!string.IsNullOrEmpty(_smsProviderCS.Fields["method"]))
                method = _smsProviderCS.Fields["method"];
            else
                method = "sms.normal";

            // If external app didnt send the sender value and template also have sender as null, then get it from provider conn string.
            if (string.IsNullOrEmpty(sender))
                sender = _smsProviderCS.Fields["sender"];
                        
            var parameters = new Dictionary<string, string> {
                { "sender", sender },
                { "to", recipients },
                { "message", message },
                { "api_key", apiKey },
                { "method", method}
            };

            HttpService httpService = new HttpService();
            HttpResponseMessage response = await httpService.PostData(url, parameters);
            string apiResponse = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
                responseMsg = "SENT";
            else
                responseMsg = responseMsg + ". " + JObject.Parse(apiResponse)["message"].ToString();

            return responseMsg;
        }
    }
}
