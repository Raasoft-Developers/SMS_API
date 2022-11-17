using AutoMapper;
using SMSService.Data.Entities;
using System.Collections.Generic;

namespace SMSService.DTOS
{
    public class SMSPoolDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class SMSPoolProfile : Profile
    {
        public SMSPoolProfile()
        {
            CreateMap<SMSPoolDto, SMSPoolTable>().ReverseMap();
            CreateMap<SMSResponseDto<SMSPoolTable>, SMSResponseDto<SMSPoolDto>>();
            CreateMap<SMSResponseDto<List<SMSPoolTable>>, SMSResponseDto<List<SMSPoolDto>>>();

        }
    }
}
