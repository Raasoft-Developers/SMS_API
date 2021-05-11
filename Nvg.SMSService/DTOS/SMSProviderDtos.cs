using AutoMapper;
using Nvg.SMSService.Data.Entities;
using System.Collections.Generic;

namespace Nvg.SMSService.DTOS
{
    public class SMSProviderSettingsDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public string SMSPoolName { get; set; }
        public string SMSPoolID { get; set; }
    }

    public class SMSProviderSettingsProfile : Profile
    {
        public SMSProviderSettingsProfile()
        {
            CreateMap<SMSProviderSettingsDto, SMSProviderSettingsTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSProviderSettingsTable>, SMSResponseDto<SMSProviderSettingsDto>>();
            CreateMap<SMSResponseDto<List<SMSProviderSettingsTable>>, SMSResponseDto<List<SMSProviderSettingsDto>>>();

        }
    }

}
