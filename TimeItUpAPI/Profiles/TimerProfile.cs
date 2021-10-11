using AutoMapper;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Profiles
{
    public class TimerProfile : Profile
    {
        public TimerProfile()
        {
            CreateMap<Timer, TimerDto>();
            CreateMap<UpdateTimerDto, Timer>();
            CreateMap<CreateTimerDto, Timer>();

        }
    }
}
