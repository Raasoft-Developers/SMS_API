using AutoMapper;
using SMSService.Data.Entities;
using SMSService.Data.SMSPool;
using SMSService.Data.SMSTemplate;
using SMSService.DTOS;

namespace SMSService
{
    public class SMSTemplateInteractor : ISMSTemplateInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSTemplateRepository _smsTemplateRepository;
        private readonly ISMSPoolRepository _smsPoolRepository;

        public SMSTemplateInteractor(IMapper mapper, ISMSTemplateRepository smsTemplateRepository,
            ISMSPoolRepository smsPoolRepository)
        {
            _mapper = mapper;
            _smsTemplateRepository = smsTemplateRepository;
            _smsPoolRepository = smsPoolRepository;
        }

        public SMSResponseDto<SMSTemplateDto> AddSMSTemplate(SMSTemplateDto templateInput)
        {
            var response = new SMSResponseDto<SMSTemplateDto>();
            if (!string.IsNullOrEmpty(templateInput.SMSPoolName))
            {
                if (string.IsNullOrEmpty(templateInput.SMSPoolID))
                {
                    var smsPool = _smsPoolRepository.GetSMSPoolByName(templateInput.SMSPoolName)?.Result;
                    if (smsPool != null)
                        templateInput.SMSPoolID = smsPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS pool.";
                        response.Result = templateInput;
                        return response;
                    }
                }
                else
                {
                    var smsPool = _smsPoolRepository.CheckIfSmsPoolIDNameValid(templateInput.SMSPoolID, templateInput.SMSPoolName);
                    if (!smsPool.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Pool ID and Pool Name do not match.";
                        response.Result = templateInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(templateInput.SMSPoolID))
            {
                var smsPool = _smsPoolRepository.CheckIfSmsPoolIDIsValid(templateInput.SMSPoolID);
                if (!smsPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Pool ID.";
                    response.Result = templateInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "SMS pool cannot be blank.";
                response.Result = templateInput;
                return response;
            }
            var mappedInput = _mapper.Map<SMSTemplateTable>(templateInput);
            var mappedResponse = _smsTemplateRepository.AddSMSTemplate(mappedInput);
            response = _mapper.Map<SMSResponseDto<SMSTemplateDto>>(mappedResponse);
            return response;
        }

        public SMSResponseDto<SMSTemplateDto> UpdateSMSTemplate(SMSTemplateDto templateInput)
        {
            var response = new SMSResponseDto<SMSTemplateDto>();
            if (!string.IsNullOrEmpty(templateInput.SMSPoolName))
            {
                if (string.IsNullOrEmpty(templateInput.SMSPoolID))
                {
                    var smsPool = _smsPoolRepository.GetSMSPoolByName(templateInput.SMSPoolName)?.Result;
                    if (smsPool != null)
                        templateInput.SMSPoolID = smsPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS pool.";
                        response.Result = templateInput;
                        return response;
                    }
                }
                else
                {
                    var smsPool = _smsPoolRepository.CheckIfSmsPoolIDNameValid(templateInput.SMSPoolID, templateInput.SMSPoolName);
                    if (!smsPool.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Pool ID and Pool Name do not match.";
                        response.Result = templateInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(templateInput.SMSPoolID))
            {
                var smsPool = _smsPoolRepository.CheckIfSmsPoolIDIsValid(templateInput.SMSPoolID);
                if (!smsPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Pool ID.";
                    response.Result = templateInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "SMS pool cannot be blank.";
                response.Result = templateInput;
                return response;
            }
            var mappedInput = _mapper.Map<SMSTemplateTable>(templateInput);
            var mappedResponse = _smsTemplateRepository.UpdateSMSTemplate(mappedInput);
            response = _mapper.Map<SMSResponseDto<SMSTemplateDto>>(mappedResponse);
            return response;
        }

        public SMSResponseDto<bool> CheckIfTemplateExist(string channelKey, string templateName)
        {
            var response = _smsTemplateRepository.CheckIfTemplateExist(channelKey, templateName);
            return response;
        }

        public SMSTemplateDto GetSMSTemplate(string templateID)
        {
            var smsTemplate = _smsTemplateRepository.GetSMSTemplate(templateID);
            return _mapper.Map<SMSTemplateDto>(smsTemplate);
        }

        public SMSTemplateDto GetSMSTemplate(string templateName, string channelKey, string variant = null)
        {
            var smsTemplate = _smsTemplateRepository.GetSMSTemplate(templateName, channelKey, variant);
            return _mapper.Map<SMSTemplateDto>(smsTemplate);
        }

    }
}
