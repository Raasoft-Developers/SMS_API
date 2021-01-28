using AutoMapper;
using Nvg.SMSService.Data;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService
{
    public class SMSTemplateInteractor : ISMSTemplateInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSTemplateRepository _smsTemplateRepository;

        public SMSTemplateInteractor(IMapper mapper, ISMSTemplateRepository smsTemplateRepository)
        {
            _mapper = mapper;
            _smsTemplateRepository = smsTemplateRepository;
        }

        public SMSTemplateDto GetSMSTemplate(long id)
        {
            var smsTemplate = _smsTemplateRepository.GetSMSTemplate(id);

            return _mapper.Map<SMSTemplateDto>(smsTemplate);
        }

        public SMSTemplateDto GetSMSTemplate(string templateName, string tenantID = null, string facilityID = null)
        {
            var smsTemplate = _smsTemplateRepository.GetSMSTemplate(templateName, tenantID, facilityID);

            return _mapper.Map<SMSTemplateDto>(smsTemplate);
        }

    }
}
