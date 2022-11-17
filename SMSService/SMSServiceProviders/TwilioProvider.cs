using Microsoft.Exchange.WebServices.Auth.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SMSService.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace SMSService.SMSServiceProviders
{
    /// <summary>
    /// Twilio Provider
    /// </summary>
    public class TwilioProvider : ISMSProvider
    {

        private readonly SMSProviderConnectionString _smsProviderCS;
        private readonly ILogger<TwilioProvider> _logger;

        public TwilioProvider(SMSProviderConnectionString smsProviderCS, ILogger<TwilioProvider> logger)
        {
            _smsProviderCS = smsProviderCS;
            _logger = logger;
        }

        public async Task<SmsProviderResponse> SendSMS(string recipients, string message, string sender = null)
        {
            string responseMsg = "NOT SENT";
            var url = _smsProviderCS.Fields["url"];
            _logger.LogDebug("URL: " + url);
            var apiKey = _smsProviderCS.Fields["key"];
            if (string.IsNullOrEmpty(sender))
                sender = _smsProviderCS.Fields["sender"];
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

            SmsProviderResponse smsProviderResponse = new SmsProviderResponse
            {
                StatusMessage = responseMsg,
                Unit = null,
                SmsCost = null
            };
            return smsProviderResponse;

        }
    }
}

