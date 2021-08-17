using AutoMapper;
using Nvg.SMSService.DTOS;

namespace Nvg.API.SMS.Models
{
    public class ChannelInput
    {
        public string Key { get; set; }
        public string SMSPoolID { get; set; }
        public string SMSPoolName { get; set; }
        public string SMSProviderID { get; set; }
        public string SMSProviderName { get; set; }
        public int MonthlyQuota { get; set; }
        public int TotalQuota { get; set; }
        //public string CurrentMonth { get; set; }
        public bool IsRestrictedByQuota { get; set; }
    }

    /// <summary>
    /// Input model for managmement controller
    /// </summary>
    public class ChannelMgmtInput : ChannelInput
    {
        public string ID { get; set; }
    }

    public class SMSChannelInputProfile : Profile
    {
        public SMSChannelInputProfile()
        {
            CreateMap<ChannelInput, SMSChannelDto>().ReverseMap();
            CreateMap<ChannelMgmtInput, SMSChannelDto>().ReverseMap();
        }
    }
}
