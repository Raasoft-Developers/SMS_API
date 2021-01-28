using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Nvg.SMSService.Data;

namespace Nvg.SMSService
{
    public class SMSCounterInteractor : ISMSCounterInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSCounterRepository _smsCountRepository;

        public SMSCounterInteractor(IMapper mapper, ISMSCounterRepository smsCountRepository)
        {
            _mapper = mapper;
            _smsCountRepository = smsCountRepository;
        }

        public string GetSMSCounter(string tenantID, string facilityID)
        {
            return _smsCountRepository.GetSMSCount(tenantID, facilityID);
        }

        public void UpdateSMSCounter(string tenantID, string facilityID)
        {
            _smsCountRepository.UpdateSMSCounter(tenantID, facilityID);
        }
    }
}
