using AutoMapper;
using SMSService.DTOS;

namespace API.SMS.Models
{
    public class TemplateInput
    {
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Sender { get; set; }
        public string SMSPoolID { get; set; }
        public string SMSPoolName { get; set; }
        public string MessageTemplate { get; set; }
    }

    /// <summary>
    /// Input model for managmement controller
    /// </summary>
    public class TemplateMgmtInput : TemplateInput
    {
        public string ID { get; set; }
    }

    public class SMSTemplateInputProfile : Profile
    {
        public SMSTemplateInputProfile()
        {
            CreateMap<TemplateInput, SMSTemplateDto>().ReverseMap();
            CreateMap<TemplateMgmtInput, SMSTemplateDto>().ReverseMap();
        }
    }
}
