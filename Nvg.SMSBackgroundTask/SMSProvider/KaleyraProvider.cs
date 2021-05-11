using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Nvg.SMSBackgroundTask.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nvg.SMSBackgroundTask.SMSProvider
{
    /// <summary>
    /// Kaleyra SMS Provider.
    /// </summary>
    public class KaleyraProvider : ISMSProvider
    {
        private readonly SMSProviderConnectionString _smsProviderCS;
        private readonly ILogger<KaleyraProvider> _logger;

        public KaleyraProvider(SMSProviderConnectionString smsProviderConnectionString, ILogger<KaleyraProvider> logger)
        {
            _smsProviderCS = smsProviderConnectionString;
            _logger = logger;
        }

        public async Task<string> SendSMS(string recipients, string message, string sender = null)
        {
            string responseMsg = "NOT SENT";

            var url = _smsProviderCS.Fields["url"];
            _logger.LogDebug("URL: " + url);
            var apiKey = _smsProviderCS.Fields["key"];
            _logger.LogDebug("APIKey: "+apiKey);
            // If external app didnt send the sender value and template also have sender as null, then get it from provider conn string.
            if (string.IsNullOrEmpty(sender))
                sender = _smsProviderCS.Fields["sender"];
            _logger.LogDebug("sender: " + sender);
            var parameters = new Dictionary<string, string> {
                { "sender", sender },
                { "to", recipients },
                { "body", message }
            };
            var headers = new Dictionary<string, string>() {
                { "api-key", apiKey }
            };
            HttpService httpService = new HttpService();
            HttpResponseMessage response = await httpService.Post(url,
                parameters,
                headers);
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
