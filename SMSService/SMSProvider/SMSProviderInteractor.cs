using AutoMapper;
using SMSService.Data.Entities;
using SMSService.Data.SMSPool;
using SMSService.Data.SMSProvider;
using SMSService.DTOS;
using System.Collections.Generic;

namespace SMSService.SMSProvider
{
    public class SMSProviderInteractor : ISMSProviderInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSProviderRepository _smsProviderRepository;
        private readonly ISMSPoolRepository _smsPoolRepository;

        public SMSProviderInteractor(IMapper mapper, ISMSProviderRepository smsProviderRepository, ISMSPoolRepository smsPoolRepository)
        {
            _mapper = mapper;
            _smsProviderRepository = smsProviderRepository;
            _smsPoolRepository = smsPoolRepository;
        }

        public SMSResponseDto<SMSProviderSettingsDto> AddSMSProvider(SMSProviderSettingsDto providerInput)
        {
            var response = new SMSResponseDto<SMSProviderSettingsDto>();
            if (!string.IsNullOrEmpty(providerInput.SMSPoolName))
            {
                if (string.IsNullOrEmpty(providerInput.SMSPoolID))
                {
                    var smsPool = _smsPoolRepository.GetSMSPoolByName(providerInput.SMSPoolName)?.Result;
                    if (smsPool != null)
                        providerInput.SMSPoolID = smsPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS pool.";
                        response.Result = providerInput;
                        return response;
                    }
                }
                else
                {
                    var smsPool = _smsPoolRepository.CheckIfSmsPoolIDNameValid(providerInput.SMSPoolID, providerInput.SMSPoolName);
                    if (!smsPool.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Pool ID and Name do not match.";
                        response.Result = providerInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(providerInput.SMSPoolID))
            {
                var smsPool = _smsPoolRepository.CheckIfSmsPoolIDIsValid(providerInput.SMSPoolID);
                if (!smsPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Pool ID.";
                    response.Result = providerInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "SMS pool cannot be blank.";
                response.Result = providerInput;
                return response;
            }
            var mappedSMSInput = _mapper.Map<SMSProviderSettingsTable>(providerInput);
            var mappedResponse = _smsProviderRepository.AddSMSProvider(mappedSMSInput);
            response = _mapper.Map<SMSResponseDto<SMSProviderSettingsDto>>(mappedResponse);
            return response;
        }

        public SMSResponseDto<SMSProviderSettingsDto> UpdateSMSProvider(SMSProviderSettingsDto providerInput)
        {
            var response = new SMSResponseDto<SMSProviderSettingsDto>();
            if (!string.IsNullOrEmpty(providerInput.SMSPoolName))
            {
                if (string.IsNullOrEmpty(providerInput.SMSPoolID))
                {
                    var smsPool = _smsPoolRepository.GetSMSPoolByName(providerInput.SMSPoolName)?.Result;
                    if (smsPool != null)
                        providerInput.SMSPoolID = smsPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS pool.";
                        response.Result = providerInput;
                        return response;
                    }
                }
                else
                {
                    var smsPool = _smsPoolRepository.CheckIfSmsPoolIDNameValid(providerInput.SMSPoolID, providerInput.SMSPoolName);
                    if (!smsPool.Status)
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS Pool ID and Name.";
                        response.Result = providerInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(providerInput.SMSPoolID))
            {
                var smsPool = _smsPoolRepository.CheckIfSmsPoolIDIsValid(providerInput.SMSPoolID);
                if (!smsPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Pool ID.";
                    response.Result = providerInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "SMS pool cannot be blank.";
                response.Result = providerInput;
                return response;
            }
            var mappedSMSInput = _mapper.Map<SMSProviderSettingsTable>(providerInput);
            var mappedResponse = _smsProviderRepository.UpdateSMSProvider(mappedSMSInput);
            response = _mapper.Map<SMSResponseDto<SMSProviderSettingsDto>>(mappedResponse);
            return response;
        }

        public SMSResponseDto<SMSProviderSettingsDto> GetSMSProviderByChannel(string channelKey)
        {
            var provider = _smsProviderRepository.GetSMSProviderByChannelKey(channelKey);
            return _mapper.Map<SMSResponseDto<SMSProviderSettingsDto>>(provider);
        }

        public SMSResponseDto<List<SMSProviderSettingsDto>> GetSMSProvidersByPool(string poolName, string providerName)
        {
            var providers = _smsProviderRepository.GetSMSProvidersByPool(poolName, providerName);
            return _mapper.Map<SMSResponseDto<List<SMSProviderSettingsDto>>>(providers);
        }
    }
}
