using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.Data.SMSPool
{
    public interface ISMSPoolRepository
    {
        SMSResponseDto<SMSPoolTable> AddSMSPool(SMSPoolTable smsPoolInput);
        SMSResponseDto<SMSPoolTable> GetSMSPoolByName(string poolName);
        SMSResponseDto<string> CheckIfSmsPoolIDIsValid(string poolID);
        SMSResponseDto<string> CheckIfSmsPoolIDNameMatch(string poolID, string poolName);
    }
}
