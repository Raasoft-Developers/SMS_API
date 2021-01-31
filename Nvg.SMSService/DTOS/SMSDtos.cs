using AutoMapper;
using Nvg.SMSService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nvg.SMSService.DTOS
{
    public class SMSDto
    {
        public string To { get; set; }
        public string Content { get; set; }
        public string Template { get; set; }
        public string Subject { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Username { get; set; }
    }

    public class SMSTemplateDto
    {
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string Name { get; set; }
        public string MessageTemplate { get; set; }
    }

    public class SMSHistoryDto
    {
        public long ID { get; set; }
        public string TenantID { get; set; }
        public string FacilityID { get; set; }
        public string ToPhNumbers { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime SentOn { get; set; }
        public string Status { get; set; }
    }

    public class SMSCounterDto
    {
        public long ID { get; set; }
        public string TenantID { get; set; }
        public int Count { get; set; }
        public string FacilityID { get; set; }
    }


    public class SMSDTOProfile : Profile
    {
        public SMSDTOProfile()
        {
            CreateMap<SMSTemplateTable, SMSTemplateDto>();
            CreateMap<SMSTemplateDto, SMSTemplateTable>();
            CreateMap<SMSHistoryTable, SMSHistoryDto>();
            CreateMap<SMSHistoryDto, SMSHistoryTable>();

            CreateMap<SMSCounterDto, SMSCounterTable>().ReverseMap();
        }
    }

}
