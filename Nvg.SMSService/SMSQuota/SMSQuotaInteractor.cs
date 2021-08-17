using AutoMapper;
using Microsoft.Extensions.Logging;
using Nvg.SMSService.Data.SMSChannel;
using Nvg.SMSService.Data.SMSQuota;
using Nvg.SMSService.DTOS;
using System;

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

        public SMSResponseDto<SMSQuotaDto> IncrementSMSQuota(string channelKey)
        {
            _logger.LogInformation("UpdateSMSQuota interactor method.");
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                var channelID = _smsChannelRepository.GetSMSChannelByKey(channelKey)?.Result?.ID;
                var smsQuotaResponse = _smsQuotaRepository.IncrementSMSQuota(channelID);
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

        public bool CheckIfQuotaExceeded(string channelKey)
        {
            _logger.LogInformation("CheckIfQuotaExceeded interactor method.");
            var response = false;
            try
            {
                var smsQuotaResponse = _smsQuotaRepository.GetSMSQuota(channelKey);
                if (smsQuotaResponse.Status && smsQuotaResponse.Result != null)
                {
                    var smsQuota = smsQuotaResponse.Result;
                    var currentMonth = DateTime.Now.ToString("MMM").ToUpper();
                    //Check if Quota is set for current month
                    if (smsQuota.CurrentMonth == currentMonth)
                    {
                        //Check if quota is exceeded for current month
                        if (smsQuota.TotalQuota != -1 && (smsQuota.MonthlyConsumption >= smsQuota.MonthlyQuota || smsQuota.TotalConsumption >= smsQuota.TotalQuota))
                        {
                            response = true;
                        }
                    }
                    else
                    {   //Reset Quota for Current month and Update the Current Month
                        var updatedQuotaResponse = _smsQuotaRepository.UpdateCurrentMonth(channelKey, currentMonth);
                        if (updatedQuotaResponse.Status)
                        {
                            _logger.LogDebug("Status: " + updatedQuotaResponse.Status + ", Message: " + updatedQuotaResponse.Message);
                            smsQuota = updatedQuotaResponse.Result;
                            //Check if quota is exceeded for current month
                            if (smsQuota.TotalQuota != -1 && (smsQuota.MonthlyConsumption >= smsQuota.MonthlyQuota || smsQuota.TotalConsumption >= smsQuota.TotalQuota))
                            {
                                response = true;
                            }
                        }
                        else
                        {
                            throw new Exception(updatedQuotaResponse.Message);
                        }
                    }
                }
                _logger.LogDebug("Status: " + smsQuotaResponse.Status + ", Message: " + smsQuotaResponse.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get SMS Quota" + ex.Message);
                response = true;
                return response;
            }
        }
        public SMSResponseDto<SMSQuotaDto> AddSMSQuota(SMSChannelDto smsChannelDto)
        {
            _logger.LogInformation("AddSMSQuota interactor method.");
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                var smsQuotaResponse = _smsQuotaRepository.AddSMSQuota(smsChannelDto);
                _logger.LogDebug("Status: " + smsQuotaResponse.Status + "Message:" + smsQuotaResponse.Message);
                var mappedResponse = _mapper.Map<SMSResponseDto<SMSQuotaDto>>(smsQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Add SMS Quota" + ex.Message);
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(SMSChannelDto smsChannelDto)
        {
            _logger.LogInformation("UpdateSMSQuota interactor method.");
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                if (smsChannelDto.IsRestrictedByQuota && (smsChannelDto.MonthlyQuota == 0 || smsChannelDto.TotalQuota == 0))
                {
                    response.Status = false;
                    response.Message = "Monthly quota and/or Total quota cannot have value as 0. Quota has not been updated in the database.";
                    _logger.LogDebug("Status: " + response.Status + "Message:" + response.Message);
                    return response;
                }
                var smsQuotaResponse = _smsQuotaRepository.UpdateSMSQuota(smsChannelDto);
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
