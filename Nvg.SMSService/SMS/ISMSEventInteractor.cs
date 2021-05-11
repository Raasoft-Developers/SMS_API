using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMS
{
    public interface ISMSEventInteractor
    {
        void SendSMS(SMSDto smsInputs);
    }
}
