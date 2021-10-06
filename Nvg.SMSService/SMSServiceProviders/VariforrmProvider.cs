using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nvg.SMSService.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nvg.SMSService.SMSServiceProviders
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

        public async Task<SmsProviderResponse> SendSMS(string recipients, string message, string sender = null)
        {
            string method;

            var url = _smsProviderCS.Fields["url"];
            var apiKey = _smsProviderCS.Fields["key"];
            _logger.LogDebug("URL: " + url);
            //_logger.LogDebug("APIKey: " + apiKey);
            if (!string.IsNullOrEmpty(_smsProviderCS.Fields["method"]))
                method = _smsProviderCS.Fields["method"];
            else
                method = "T";
            //_logger.LogDebug("method: " + method);
            // If external app didnt send the sender value and template also have sender as null, then get it from provider conn string.
            if (string.IsNullOrEmpty(sender))
                sender = _smsProviderCS.Fields["sender"];
            //_logger.LogDebug("sender: " + sender);
            //var parameters = new Dictionary<string, string> {
            //    { "sender", sender },
            //    { "to", recipients },
            //    { "message", message },
            //    { "api_key", apiKey },
            //    { "method", method}
            //};

            var parameters = new Dictionary<string, string> {
                { "sender", sender },
                { "to", recipients },
                { "message", message },
                { "service", method }
            };
            var headers = new Dictionary<string, string>() {
                { "Authorization", "Bearer " + apiKey }
            };

            HttpService httpService = new HttpService();
            HttpResponseMessage response = await httpService.Post(url, parameters, headers);
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            string statusMessage = "NOT SENT";
            dynamic result;
            if (response.IsSuccessStatusCode)
            {
                JObject smsResponse = JObject.Parse(apiResponse);
                
                if (smsResponse.ContainsKey("data"))
                {
                    statusMessage = "SENT";
                    _logger.LogInformation(statusMessage);

                    result = JsonConvert.DeserializeObject<dynamic>(smsResponse["data"].ToString());
                }
                else
                {
                    statusMessage = "NOT SENT." + smsResponse["message"].ToString();
                    result = null;
                    _logger.LogInformation("NOT SENT." + smsResponse["message"].ToString());
                }
            }
            else
            {
                result = null;
                _logger.LogError("Could not send SMS. " + JObject.Parse(apiResponse)["message"].ToString());
                statusMessage = "NOT SENT. " + JObject.Parse(apiResponse)["message"].ToString();
            }

            SmsProviderResponse smsProviderResponse = new SmsProviderResponse
            {
                StatusMessage = statusMessage,
                Unit = result != null? result[0].units.Value : null,
                SmsCost = result != null ?  result[0].charges.Value: null
            };

            return smsProviderResponse;
        }
    }
}
