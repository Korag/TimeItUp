using AutoMapper;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserRegisterDto, BasicIdentityUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email));
            
            CreateMap<BasicIdentityUser, User>()
                 .ForMember(dest => dest.EmailAddress, opts => opts.MapFrom(src => src.Email));
        }
    }
}
