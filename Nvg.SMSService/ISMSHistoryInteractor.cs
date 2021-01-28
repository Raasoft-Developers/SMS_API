
using Nvg.SMSService.DTOS;

namespace Nvg.SMSService
{
    public interface ISMSHistoryInteractor
    {
        SMSHistoryDto Add(SMSHistoryDto sms);
    }
}