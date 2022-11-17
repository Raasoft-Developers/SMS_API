using AutoMapper;
using Microsoft.Extensions.Logging;
using SMSService.Data.SMSChannel;
using SMSService.Data.SMSQuota;
using SMSService.DTOS;
using System;

namespace SMSService.SMSQuota
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

        public SMSResponseDto<SMSQuotaDto> IncrementSMSQuota(string channelKey,long creditsUsed)
        {
            _logger.LogInformation("UpdateSMSQuota interactor method.");
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                var channelID = _smsChannelRepository.GetSMSChannelByKey(channelKey)?.Result?.ID;
                var smsQuotaResponse = _smsQuotaRepository.IncrementSMSQuota(channelID, creditsUsed);
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

        public SMSBalanceDto CheckIfQuotaExceeded(string channelKey)
        {
            _logger.LogInformation("CheckIfQuotaExceeded interactor method.");
            var response = new SMSBalanceDto();
            try
            {
                var smsQuotaResponse = _smsQuotaRepository.GetSMSQuota(channelKey);
                if (smsQuotaResponse.Status && smsQuotaResponse.Result != null)
                {
                    var smsQuota = smsQuotaResponse.Result;
                    var currentMonth = DateTime.Now.ToString("MMM").ToUpper();
                    //Check if Current Month in Table is set to the actual current month else update the table value
                    if (smsQuota.CurrentMonth != currentMonth)
                    {
                        var updatedQuotaResponse = _smsQuotaRepository.UpdateCurrentMonth(channelKey, currentMonth);
                        if (updatedQuotaResponse.Status)
                        {
                            _logger.LogDebug("Status: " + updatedQuotaResponse.Status + ", Message: " + updatedQuotaResponse.Message);
                            smsQuota = updatedQuotaResponse.Result;
                        }
                    }
                    if (smsQuota.TotalQuota != -1)
                    {
                        response.HasLimit = true;
                        response.Balance = smsQuota.TotalQuota - smsQuota.TotalConsumption;
                        //Replace Balance with the lower value from the Total and Monthly Balance
                        var monthlyBalance = smsQuota.MonthlyQuota - smsQuota.MonthlyConsumption;
                        if (response.Balance > monthlyBalance)
                        {
                            response.Balance = monthlyBalance;
                        }
                        if (smsQuota.MonthlyConsumption >= smsQuota.MonthlyQuota || smsQuota.TotalConsumption >= smsQuota.TotalQuota)
                        {
                            response.IsExceeded = true;
                        }
                    }
                }
                _logger.LogDebug("Status: " + smsQuotaResponse.Status + ", Message: " + smsQuotaResponse.Message);
                _logger.LogDebug("IsExceed: " + response.IsExceeded + ", Balance: " + response.Balance + ", HasLimit: " + response.HasLimit);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get SMS Quota" + ex.Message);
                response.IsExceeded = true;
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
