using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nvg.SMSBackgroundTask.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nvg.SMSBackgroundTask.SMSProvider
{
    /// <summary>
    /// Variforrm SMS Provider.
    /// </summary>
    public class VariforrmProvider : ISMSProvider
    {
        private readonly SMSProviderConnectionString _smsProviderCS;
        private readonly ILogger<VariforrmProvider> _logger;

        public VariforrmProvider(SMSProviderConnectionString smsProviderConnectionString, ILogger<VariforrmProvider> logger)
        {
            _smsProviderCS = smsProviderConnectionString;
            _logger = logger;
        }

        public async Task<string> SendSMS(string recipients, string message, string sender = null)
        {
            string responseMsg = "NOT SENT";
            string method;

            var url = _smsProviderCS.Fields["url"];
            var apiKey = _smsProviderCS.Fields["key"];
            _logger.LogDebug("URL: " + url);
            _logger.LogDebug("APIKey: " + apiKey);
            if (!string.IsNullOrEmpty(_smsProviderCS.Fields["method"]))
                method = _smsProviderCS.Fields["method"];
            else
                method = "sms.normal";
            _logger.LogDebug("method: " + method);
            // If external app didnt send the sender value and template also have sender as null, then get it from provider conn string.
            if (string.IsNullOrEmpty(sender))
                sender = _smsProviderCS.Fields["sender"];
            _logger.LogDebug("sender: " + sender);
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
            {
                _logger.LogInformation("SMS Sent");
                responseMsg = "SENT";
            }
            else
            {
                _logger.LogError("Could not send SMS. " + JObject.Parse(apiResponse)["message"].ToString());
                responseMsg = responseMsg + ". " + JObject.Parse(apiResponse)["message"].ToString();
            }

            return responseMsg;
        }
    }
}
