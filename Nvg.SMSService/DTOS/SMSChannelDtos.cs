using AutoMapper;
using Nvg.SMSService.Data.Entities;
using System.Collections.Generic;

namespace Nvg.SMSService.DTOS
{
    public class SMSChannelDto
    {
        public string ID { get; set; }
        public string Key { get; set; }
        public string SMSPoolID { get; set; }
        public string SMSPoolName { get; set; }
        public string SMSProviderID { get; set; }
        public string SMSProviderName { get; set; }
        public int MonthlyQuota { get; set; }
        public int TotalQuota { get; set; }
        public int MonthlyConsumption { get; set; }
        public int TotalConsumption { get; set; }
        public string CurrentMonth { get; set; }
        public bool IsRestrictedByQuota { get; set; }
    }

    public class SMSChannelProfile : Profile
    {
        public SMSChannelProfile()
        {
            CreateMap<SMSChannelDto, SMSChannelTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSChannelTable>, SMSResponseDto<SMSChannelDto>>();
            CreateMap<SMSResponseDto<List<SMSChannelTable>>, SMSResponseDto<List<SMSChannelDto>>>();
        }
    }

}
