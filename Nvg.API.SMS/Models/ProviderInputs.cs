using AutoMapper;
using Nvg.SMSService.DTOS;

namespace Nvg.API.SMS.Models
{
    public class ProviderInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Configuration { get; set; }
        public string SMSPoolName { get; set; }
        public string SMSPoolID { get; set; }
    }

    /// <summary>
    /// Input model for Management controller
    /// </summary>
    public class ProviderMgmtInput : ProviderInput
    {
        public string ID { get; set; }
    }

    public class SMSProviderInputProfile : Profile
    {
        public SMSProviderInputProfile()
        {
            CreateMap<ProviderInput, SMSProviderSettingsDto>().ReverseMap();
            CreateMap<ProviderMgmtInput, SMSProviderSettingsDto>().ReverseMap();
        }
    }
}
