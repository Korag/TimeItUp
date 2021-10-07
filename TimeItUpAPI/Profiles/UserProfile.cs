using TimeItUpAPI.Models;
using TimeItUpData.Library.Models;
using AutoMapper;

namespace TimeItUpAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<AddUserDto, User>();
        }
    }
}
