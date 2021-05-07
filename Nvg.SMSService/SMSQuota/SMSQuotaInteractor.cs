using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.Data.SMSChannel;
using Nvg.SMSService.Data.SMSQuota;
using Nvg.SMSService.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.SMSQuota
{
    public class SMSQuotaInteractor : ISMSQuotaInteractor
    {
        private readonly IMapper _mapper;
        private readonly ISMSQuotaRepository _smsQuotaRepository;
        private readonly ISMSChannelRepository _smsChannelRepository;
        private readonly ILogger<SMSQuotaInteractor> _logger;

        public SMSQuotaInteractor(IMapper mapper, ISMSQuotaRepository smsQuotaRepository,
            ISMSChannelRepository smsChannelRepository, ILogger<SMSQuotaInteractor> logger)
        {
            _mapper = mapper;
            _smsQuotaRepository = smsQuotaRepository;
            _smsChannelRepository = smsChannelRepository;
            _logger = logger;
        }

        public SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey)
        {
            _logger.LogInformation("GetSMSQuota interactor method.");
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                //var channelID = _smsChannelRepository.GetSMSChannelByKey(channelKey)?.Result?.ID;
                var smsQuotaResponse = _smsQuotaRepository.GetSMSQuota(channelKey);
                _logger.LogDebug("Status: " + smsQuotaResponse.Status + ", Message: " + smsQuotaResponse.Message);
                var mappedResponse = _mapper.Map<SMSResponseDto<SMSQuotaDto>>(smsQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get SMS Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(string channelKey)
        {
            _logger.LogInformation("UpdateSMSQuota interactor method.");
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                var channelID = _smsChannelRepository.GetSMSChannelByKey(channelKey)?.Result?.ID;
                var smsQuotaResponse = _smsQuotaRepository.UpdateSMSQuota(channelID);
                _logger.LogDebug("Status: " + smsQuotaResponse.Status + "Message:" + smsQuotaResponse.Message);
                var mappedResponse = _mapper.Map<SMSResponseDto<SMSQuotaDto>>(smsQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Update SMS Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
