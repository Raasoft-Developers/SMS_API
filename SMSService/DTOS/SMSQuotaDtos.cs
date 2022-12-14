using AutoMapper;
using SMSService.Data.Entities;
using System.Collections.Generic;

namespace SMSService.DTOS
{
    public class SMSQuotaDto
    {
        public long ID { get; set; }
        public string SMSChannelID { get; set; }
        public string SMSChannelKey { get; set; }
        public int TotalConsumption { get; set; }
        public int TotalQuota { get; set; }
        public int MonthlyConsumption { get; set; }
        public string CurrentMonth { get; set; }
        public int MonthlyQuota { get; set; }
    }

    public class SMSQuotaProfile : Profile
    {
        public SMSQuotaProfile()
        {
            CreateMap<SMSQuotaDto, SMSQuotaTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSQuotaTable>, SMSResponseDto<SMSQuotaDto>>();
            CreateMap<SMSResponseDto<List<SMSQuotaTable>>, SMSResponseDto<List<SMSQuotaDto>>>();

        }
    }
}
