using AutoMapper;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Profiles
{
    public class AlarmProfile : Profile
    {
        public AlarmProfile()
        {
            CreateMap<Alarm, AlarmDto>();
            CreateMap<UpdateAlarmDto, Alarm>();
            CreateMap<CreateAlarmDto, Alarm>();
        }
    }
}
