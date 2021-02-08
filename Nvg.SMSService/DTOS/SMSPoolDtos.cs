﻿using AutoMapper;
using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.DTOS
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

        }
    }
}
