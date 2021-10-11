using AutoMapper;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Profiles
{
    public class SplitProfile : Profile
    {
        public SplitProfile()
        {
            CreateMap<Split, SplitDto>();
            CreateMap<CreateSplitDto, Split>();
        }
    }
}
