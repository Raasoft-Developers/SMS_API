using AutoMapper;
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

        public SMSQuotaInteractor(IMapper mapper, ISMSQuotaRepository smsQuotaRepository,
            ISMSChannelRepository smsChannelRepository)
        {
            _mapper = mapper;
            _smsQuotaRepository = smsQuotaRepository;
            _smsChannelRepository = smsChannelRepository;
        }

        public SMSResponseDto<SMSQuotaDto> GetSMSQuota(string channelKey)
        {
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                //var channelID = _smsChannelRepository.GetSMSChannelByKey(channelKey)?.Result?.ID;
                var smsQuotaResponse = _smsQuotaRepository.GetSMSQuota(channelKey);
                var mappedResponse = _mapper.Map<SMSResponseDto<SMSQuotaDto>>(smsQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public SMSResponseDto<SMSQuotaDto> UpdateSMSQuota(string channelKey)
        {
            var response = new SMSResponseDto<SMSQuotaDto>();
            try
            {
                var channelID = _smsChannelRepository.GetSMSChannelByKey(channelKey)?.Result?.ID;
                var smsQuotaResponse = _smsQuotaRepository.UpdateSMSQuota(channelID);
                var mappedResponse = _mapper.Map<SMSResponseDto<SMSQuotaDto>>(smsQuotaResponse);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
