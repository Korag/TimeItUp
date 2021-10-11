using AutoMapper;
using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
