﻿using AutoMapper;
using Nvg.SMSService.Data.Entities;

namespace Nvg.SMSService.DTOS
{
    public class SMSQuotaDto
    {
        public long ID { get; set; }
        public string ChannelID { get; set; }
        public int TotalConsumption { get; set; }
        public int MonthylConsumption { get; set; }
        public int MonthlyQuota { get; set; }
    }

    public class SMSQuotaProfile : Profile
    {
        public SMSQuotaProfile()
        {
            CreateMap<SMSQuotaDto, SMSQuotaTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSQuotaTable>, SMSResponseDto<SMSQuotaDto>>();

        }
    }
}
