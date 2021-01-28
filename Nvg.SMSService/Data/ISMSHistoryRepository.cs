using Nvg.SMSService.Data.Entities;

namespace Nvg.SMSService.Data
{
    public interface ISMSHistoryRepository
    {
        SMSHistoryTable Add(SMSHistoryTable sms);
    }
}