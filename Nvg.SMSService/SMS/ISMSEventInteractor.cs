using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMS
{
    public interface ISMSEventInteractor
    {
        /// <summary>
        /// Sends the SMS to rabbitmq queue.
        /// </summary>
        /// <param name="smsInputs"><see cref="SMSDto"/> model</param>
        void SendSMS(SMSDto smsInputs);
    }
}
