using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<User, UserGetModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();
        }
    }
}