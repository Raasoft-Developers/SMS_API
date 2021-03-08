using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.Data.Entities;
using Nvg.SMSService.Data.SMSChannel;
using Nvg.SMSService.Data.SMSHistory;
using Nvg.SMSService.Data.SMSProvider;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSHistory
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
                        historyInput.SMSProviderID = _smsProviderRepository.GetSMSProviderByName(historyInput.ProviderName)?.Result?.ID;

                    _logger.LogDebug($"EmailProviderID: {historyInput.SMSProviderID}");

                }
                if (!string.IsNullOrEmpty(historyInput.ChannelKey))
                {
                    _logger.LogDebug($"ChannelKey: {historyInput.ChannelKey}");
                    if (string.IsNullOrEmpty(historyInput.SMSChannelID))
                        historyInput.SMSChannelID = _smsChannelRepository.GetSMSChannelByKey(historyInput.ChannelKey)?.Result?.ID;
                    _logger.LogDebug($"EmailChannelID: {historyInput.SMSChannelID}");
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
