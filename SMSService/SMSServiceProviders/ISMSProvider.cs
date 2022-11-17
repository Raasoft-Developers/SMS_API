using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMSService.SMSServiceProviders
{
    public interface ISMSProvider
    {
        /// <summary>
        /// Method to send SMS to the user.
        /// </summary>
        /// <param name="recipients">Recipients(Comma seperated if multiple)</param>
        /// <param name="message">Message Body</param>
        /// <param name="sender">Sender</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public Task<SmsProviderResponse> SendSMS(string recipients, string message, string sender = "");
    }
}
