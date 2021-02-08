using AutoMapper;
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

        public SMSHistoryInteractor(IMapper mapper, ISMSHistoryRepository smsHistoryRepository,
            ISMSProviderRepository smsProviderRepository, ISMSChannelRepository smsChannelRepository)
        {
            _mapper = mapper;
            _smsHistoryRepository = smsHistoryRepository;
            _smsProviderRepository = smsProviderRepository;
            _smsChannelRepository = smsChannelRepository;
        }

        public SMSResponseDto<SMSHistoryDto> AddSMSHistory(SMSHistoryDto historyInput)
        {
            var response = new SMSResponseDto<SMSHistoryDto>();
            if (!string.IsNullOrEmpty(historyInput.ProviderName))
            {
                if (string.IsNullOrEmpty(historyInput.SMSProviderID))
                    historyInput.SMSProviderID = _smsProviderRepository.GetSMSProviderByName(historyInput.ProviderName)?.Result?.ID;
            }
            if (!string.IsNullOrEmpty(historyInput.ChannelKey))
            {
                if (string.IsNullOrEmpty(historyInput.SMSChannelID))
                    historyInput.SMSChannelID = _smsChannelRepository.GetSMSChannelByKey(historyInput.ChannelKey)?.Result?.ID;
            }
            var mappedSMSInput = _mapper.Map<SMSHistoryTable>(historyInput);
            var mappedResponse = _smsHistoryRepository.AddSMSHistory(mappedSMSInput);
            response = _mapper.Map<SMSResponseDto<SMSHistoryDto>>(mappedResponse);
            return response;
        }

    }
}
