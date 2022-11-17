using AutoMapper;
using SMSService.Data.Entities;
using SMSService.Data.SMSChannel;
using SMSService.Data.SMSPool;
using SMSService.Data.SMSProvider;
using SMSService.DTOS;

namespace SMSService.SMSChannel
{
    public class SMSChannelInteractor : ISMSChannelInteractor
    {
        private readonly ISMSChannelRepository _smsChannelRepository;
        private readonly IMapper _mapper;
        private readonly ISMSProviderRepository _smsProviderRepository;
        private readonly ISMSPoolRepository _smsPoolRepository;
        private readonly string defaultSMSProvider = "default-provider";

        public SMSChannelInteractor(IMapper mapper, ISMSChannelRepository smsChannelRepository, ISMSProviderRepository smsProviderRepository, 
            ISMSPoolRepository smsPoolRepository)
        {
            _smsChannelRepository = smsChannelRepository;
            _mapper = mapper;
            _smsProviderRepository = smsProviderRepository;
            _smsPoolRepository = smsPoolRepository;
        }

        public SMSResponseDto<SMSChannelDto> AddSMSChannel(SMSChannelDto channelInput)
        {
            var response = new SMSResponseDto<SMSChannelDto>();
            if (!string.IsNullOrEmpty(channelInput.SMSPoolName))
            {
                if (string.IsNullOrEmpty(channelInput.SMSPoolID))
                {
                    var smsPool = _smsPoolRepository.GetSMSPoolByName(channelInput.SMSPoolName)?.Result;
                    if (smsPool != null)
                        channelInput.SMSPoolID = smsPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS pool.";
                        response.Result = channelInput;
                        return response;
                    }
                }
                else
                {
                    var smsPool = _smsPoolRepository.CheckIfSmsPoolIDNameValid(channelInput.SMSPoolID,channelInput.SMSPoolName);
                    if (!smsPool.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Pool ID and Pool Name do not match.";
                        response.Result = channelInput;
                        return response;
                    }
                }
            }            
            else if (!string.IsNullOrEmpty(channelInput.SMSPoolID))
            {
                var smsPool = _smsPoolRepository.CheckIfSmsPoolIDIsValid(channelInput.SMSPoolID);
                if (!smsPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Pool ID.";
                    response.Result = channelInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "SMS pool cannot be blank.";
                response.Result = channelInput;
                return response;
            }

            if (!string.IsNullOrEmpty(channelInput.SMSProviderName))
            {
                if (string.IsNullOrEmpty(channelInput.SMSProviderID))
                {
                    var smsProvider = _smsProviderRepository.GetSMSProviderByName(channelInput.SMSProviderName)?.Result;
                    if(smsProvider != null)
                        channelInput.SMSProviderID = smsProvider.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS provider.";
                        response.Result = channelInput;
                        return response;
                    }
                }
                else
                {
                    var smsProvider = _smsProviderRepository.CheckIfSmsProviderIDNameValid(channelInput.SMSProviderID, channelInput.SMSProviderName);
                    if (!smsProvider.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Provider ID and Provider Name do not match.";
                        response.Result = channelInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(channelInput.SMSProviderID))
            {
                var smsProvider = _smsProviderRepository.CheckIfSmsProviderIDIsValid(channelInput.SMSProviderID);
                if (!smsProvider.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Provider ID.";
                    response.Result = channelInput;
                    return response;
                }
            }
            else
                channelInput.SMSProviderID = _smsProviderRepository.GetDefaultSMSProvider()?.Result?.ID;

            var mappedSMSInput = _mapper.Map<SMSChannelTable>(channelInput);
            var mappedResponse = _smsChannelRepository.AddSMSChannel(mappedSMSInput);
            response = _mapper.Map<SMSResponseDto<SMSChannelDto>>(mappedResponse);
            return response;
        }

        public SMSResponseDto<SMSChannelDto> UpdateSMSChannel(SMSChannelDto channelInput)
        {
            var response = new SMSResponseDto<SMSChannelDto>();
            if (!string.IsNullOrEmpty(channelInput.SMSPoolName))
            {
                if (string.IsNullOrEmpty(channelInput.SMSPoolID))
                {
                    var smsPool = _smsPoolRepository.GetSMSPoolByName(channelInput.SMSPoolName)?.Result;
                    if (smsPool != null)
                        channelInput.SMSPoolID = smsPool.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS pool.";
                        response.Result = channelInput;
                        return response;
                    }
                }
                else
                {
                    var smsPool = _smsPoolRepository.CheckIfSmsPoolIDNameValid(channelInput.SMSPoolID, channelInput.SMSPoolName);
                    if (!smsPool.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Pool ID and Pool Name do not match.";
                        response.Result = channelInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(channelInput.SMSPoolID))
            {
                var smsPool = _smsPoolRepository.CheckIfSmsPoolIDIsValid(channelInput.SMSPoolID);
                if (!smsPool.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Pool ID.";
                    response.Result = channelInput;
                    return response;
                }
            }
            else
            {
                response.Status = false;
                response.Message = "SMS pool cannot be blank.";
                response.Result = channelInput;
                return response;
            }

            if (!string.IsNullOrEmpty(channelInput.SMSProviderName))
            {
                if (string.IsNullOrEmpty(channelInput.SMSProviderID))
                {
                    var smsProvider = _smsProviderRepository.GetSMSProviderByName(channelInput.SMSProviderName)?.Result;
                    if (smsProvider != null)
                        channelInput.SMSProviderID = smsProvider.ID;
                    else
                    {
                        response.Status = false;
                        response.Message = "Invalid SMS provider.";
                        response.Result = channelInput;
                        return response;
                    }
                }
                else
                {
                    var smsProvider = _smsProviderRepository.CheckIfSmsProviderIDNameValid(channelInput.SMSProviderID, channelInput.SMSProviderName);
                    if (!smsProvider.Status)
                    {
                        response.Status = false;
                        response.Message = "SMS Provider ID and Provider Name do not match.";
                        response.Result = channelInput;
                        return response;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(channelInput.SMSProviderID))
            {
                var smsProvider = _smsProviderRepository.CheckIfSmsProviderIDIsValid(channelInput.SMSProviderID);
                if (!smsProvider.Status)
                {
                    response.Status = false;
                    response.Message = "Invalid SMS Provider ID.";
                    response.Result = channelInput;
                    return response;
                }
            }
            else
                channelInput.SMSProviderID = _smsProviderRepository.GetDefaultSMSProvider()?.Result?.ID;

            var mappedSMSInput = _mapper.Map<SMSChannelTable>(channelInput);
            var mappedResponse = _smsChannelRepository.UpdateSMSChannel(mappedSMSInput);
            response = _mapper.Map<SMSResponseDto<SMSChannelDto>>(mappedResponse);
            return response;
        }

        public SMSResponseDto<SMSChannelDto> GetSMSChannelByKey(string channelKey)
        {
            var response = _smsChannelRepository.GetSMSChannelByKey(channelKey);
            var mappedResponse = _mapper.Map<SMSResponseDto<SMSChannelDto>>(response);
            return mappedResponse;
        }

        public SMSResponseDto<bool> CheckIfChannelExist(string channelKey)
        {
            var response = _smsChannelRepository.CheckIfChannelExist(channelKey);
            return response;
        }
    }
}
