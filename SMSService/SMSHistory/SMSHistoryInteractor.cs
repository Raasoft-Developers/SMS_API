using AutoMapper;
using Microsoft.Extensions.Logging;
using SMSService.Data.Entities;
using SMSService.Data.SMSChannel;
using SMSService.Data.SMSHistory;
using SMSService.Data.SMSProvider;
using SMSService.DTOS;
using System;
using System.Collections.Generic;

namespace SMSService.SMSHistory
{
    public class SMSHistoryInteractor : ISMSHistoryInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSHistoryRepository _smsHistoryRepository;
        private readonly ISMSProviderRepository _smsProviderRepository;
        private readonly ISMSChannelRepository _smsChannelRepository;
        private readonly ILogger<SMSHistoryInteractor> _logger;

        public SMSHistoryInteractor(IMapper mapper, ISMSHistoryRepository smsHistoryRepository,
            ISMSProviderRepository smsProviderRepository, ISMSChannelRepository smsChannelRepository, ILogger<SMSHistoryInteractor> logger)
        {
            _mapper = mapper;
            _smsHistoryRepository = smsHistoryRepository;
            _smsProviderRepository = smsProviderRepository;
            _smsChannelRepository = smsChannelRepository;
            _logger = logger;
        }

        public SMSResponseDto<SMSHistoryDto> AddSMSHistory(SMSHistoryDto historyInput)
        {
            _logger.LogInformation("AddSMSHistory interactor method.");
            var response = new SMSResponseDto<SMSHistoryDto>();
            try
            {
                if (!string.IsNullOrEmpty(historyInput.ProviderName))
                {
                    _logger.LogDebug($"Providername: {historyInput.ProviderName}");
                    if (string.IsNullOrEmpty(historyInput.SMSProviderID))
                    {
                        historyInput.SMSProviderID = _smsProviderRepository.GetSMSProviderByName(historyInput.ProviderName)?.Result?.ID;
                        if (string.IsNullOrEmpty(historyInput.SMSProviderID))
                        {
                            response.Status = false;
                            response.Message = $"Unable to find Provider with name {historyInput.ProviderName}";
                            response.Result = historyInput;
                            _logger.LogError($"{response.Message}");
                            return response;
                        }
                    }
                    else
                    {
                        var smsProvider = _smsProviderRepository.CheckIfSmsProviderIDNameValid(historyInput.SMSProviderID, historyInput.ProviderName);
                        if (!smsProvider.Status)
                        {
                            response.Status = false;
                            response.Message = smsProvider.Message;
                            response.Result = historyInput;
                            _logger.LogError($"{response.Message}");
                            return response;
                        }
                    }
                    _logger.LogDebug($"ProviderID: {historyInput.SMSProviderID}");

                }else if (!string.IsNullOrEmpty(historyInput.SMSProviderID))
                {
                    var smsProvider = _smsProviderRepository.CheckIfSmsProviderIDIsValid(historyInput.SMSProviderID);
                    if (!smsProvider.Status)
                    {
                        response.Status = false;
                        response.Message = smsProvider.Message;
                        response.Result = historyInput;
                        _logger.LogError($"{response.Message}");
                        return response;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS Provider ID or Name cannot be blank.";
                    response.Result = historyInput;
                    _logger.LogError($"{response.Message}");
                    return response;
                }
                if (!string.IsNullOrEmpty(historyInput.ChannelKey))
                {
                    _logger.LogDebug($"ChannelKey: {historyInput.ChannelKey}");
                    if (string.IsNullOrEmpty(historyInput.SMSChannelID))
                    {
                        historyInput.SMSChannelID = _smsChannelRepository.GetSMSChannelByKey(historyInput.ChannelKey)?.Result?.ID;
                        if (string.IsNullOrEmpty(historyInput.SMSChannelID))
                        {
                            response.Status = false;
                            response.Message = $"Unable to find Channel with Key {historyInput.ChannelKey}";
                            response.Result = historyInput;
                            _logger.LogError($"{response.Message}");
                            return response;
                        }
                    }
                    else
                    {
                        var smsChannel = _smsChannelRepository.CheckIfSmsChannelIDKeyValid(historyInput.SMSChannelID, historyInput.ChannelKey);
                        if (!smsChannel.Status)
                        {
                            _logger.LogError($"{smsChannel.Message}");
                            response.Status = false;
                            response.Message = smsChannel.Message;
                            response.Result = historyInput;
                            return response;
                        }
                    }
                    _logger.LogDebug($"ChannelID: {historyInput.SMSChannelID}");
                }
                else if (!string.IsNullOrEmpty(historyInput.SMSChannelID))
                {
                    var smsChannel = _smsChannelRepository.CheckIfSmsChannelIDIsValid(historyInput.SMSChannelID);
                    if (!smsChannel.Status)
                    {
                        _logger.LogError($"{smsChannel.Message}");
                        response.Status = false;
                        response.Message = smsChannel.Message;
                        response.Result = historyInput;
                        return response;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "SMS Channel ID or Key cannot be blank.";
                    response.Result = historyInput;
                    _logger.LogError($"{response.Message}");
                    return response;
                }
                var mappedSMSInput = _mapper.Map<SMSHistoryTable>(historyInput);
                var mappedResponse = _smsHistoryRepository.AddSMSHistory(mappedSMSInput);
                response = _mapper.Map<SMSResponseDto<SMSHistoryDto>>(mappedResponse);
                _logger.LogDebug($"Status: {response.Status}, Message: {response.Message}");
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred while adding sms history:" + ex.Message);
                response.Message = "Error occurred while adding sms history: " + ex.Message;
                response.Status = false;
                return response;
            }
        }

        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByDateRange(string channelKey, string tag, string fromDate, string toDate)
        {
            _logger.LogInformation("GetSMSHistoriesByDateRange interactor method.");
            SMSResponseDto<List<SMSHistoryDto>> responseDto = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                var histories = _smsHistoryRepository.GetSMSHistoriesByDateRange(channelKey, tag, fromDate, toDate);
                return _mapper.Map<SMSResponseDto<List<SMSHistoryDto>>>(histories);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting sms history by date range:" + ex.Message);
                responseDto.Message = "Failed to get histories by date range: " + ex.Message;
                responseDto.Status = false;
                return responseDto;
            }
        }

        public SMSResponseDto<List<SMSHistoryDto>> GetSMSHistoriesByTag(string channelKey, string tag)
        {
            _logger.LogInformation("GetSMSHistoriesByTag interactor method.");
            SMSResponseDto<List<SMSHistoryDto>> responseDto = new SMSResponseDto<List<SMSHistoryDto>>();
            try
            {
                var histories = _smsHistoryRepository.GetSMSHistoriesByTag(channelKey, tag);
                return _mapper.Map<SMSResponseDto<List<SMSHistoryDto>>>(histories);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occurred while getting sms history by tag:" + ex.Message);
                responseDto.Message = "Failed to get histories by tag: " + ex.Message;
                responseDto.Status = false;
                return responseDto;
            }
        }
    }
}
