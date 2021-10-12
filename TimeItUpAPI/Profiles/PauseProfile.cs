using AutoMapper;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Profiles
{
    public class PauseProfile : Profile
    {
        public PauseProfile()
        {
            CreateMap<Pause, PauseDto>();
            CreateMap<CreatePauseDto, Pause>();
        }
    }
}
