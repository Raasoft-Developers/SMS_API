using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMSPool
{
    public interface ISMSPoolInteractor
    {
        /// <summary>
        /// Adds the SMS Pool in the database.
        /// </summary>
        /// <param name="smsPoolInput"><see cref="SMSPoolDto"/> model</param>
        /// <returns><see cref="SMSResponseDto{T}"/> model</returns>
        SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto smsPoolInput);
    }
}
