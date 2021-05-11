using AutoMapper;
using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.Data.SMSPool;
using Nvg.SMSService.DTOS;

namespace Nvg.SMSService.SMSPool
{
    public class SMSPoolInteractor : ISMSPoolInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSPoolRepository _smsPoolRepository;

        public SMSPoolInteractor(IMapper mapper, ISMSPoolRepository smsPoolRepository)
        {
            _mapper = mapper;
            _smsPoolRepository = smsPoolRepository;
        }

        public SMSResponseDto<SMSPoolDto> AddSMSPool(SMSPoolDto smsPoolInput)
        {
            var mappedSMSInput = _mapper.Map<SMSPoolTable>(smsPoolInput);
            var response = _smsPoolRepository.AddSMSPool(mappedSMSInput);
            var mappedSMSResponse = _mapper.Map<SMSResponseDto<SMSPoolDto>>(response);
            return mappedSMSResponse;
        }
    }
}
