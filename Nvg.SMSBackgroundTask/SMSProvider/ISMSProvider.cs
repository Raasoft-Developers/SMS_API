﻿using System.Threading.Tasks;

namespace Nvg.SMSBackgroundTask.SMSProvider
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
        public Task<string> SendSMS(string recipients, string message, string sender = "");
    }
}
