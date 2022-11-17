using AutoMapper;
using SMSService.DTOS;

namespace API.SMS.Models
{
    public class PoolInput
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Input model for managmement controller
    /// </summary>
    public class PoolMgmtInput : PoolInput
    {
        public string ID { get; set; }
    }

    public class PoolInputProfile : Profile
    {
        public PoolInputProfile()
        {
            CreateMap<PoolInput, SMSPoolDto>().ReverseMap();
            CreateMap<PoolMgmtInput, SMSPoolDto>().ReverseMap();
        }
    }
}
