using SMSService.DTOS;

namespace SMSService.SMS
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
