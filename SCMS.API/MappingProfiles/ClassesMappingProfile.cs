using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class ClassesMappingProfile : Profile
    {
        public ClassesMappingProfile()
        {
            CreateMap<Class, Class>().ForMember(x => x.ClassId, opt => opt.Ignore());
            CreateMap<Class, CreateClassDto>().ReverseMap();
            CreateMap<Class, GetClassDto>().ReverseMap();
        }
    }
}
