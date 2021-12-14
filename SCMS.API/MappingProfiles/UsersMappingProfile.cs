using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
        }
    }
}