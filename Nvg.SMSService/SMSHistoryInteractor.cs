using AutoMapper;
using Nvg.SMSService.Data;
using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService
{
    public class SMSHistoryInteractor : ISMSHistoryInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSHistoryRepository _smsHistoryRepository;

        public SMSHistoryInteractor(IMapper mapper, ISMSHistoryRepository smsHistoryRepository)
        {
            _mapper = mapper;
            _smsHistoryRepository = smsHistoryRepository;
        }

        public SMSHistoryDto Add(SMSHistoryDto sms)
        {
            var smsObj = _mapper.Map<SMSHistoryTable>(sms);
            smsObj = _smsHistoryRepository.Add(smsObj);
            return _mapper.Map<SMSHistoryDto>(smsObj);
        }

    }
}
