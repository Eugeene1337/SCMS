using AutoMapper;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class ClassesMappings : Profile
    {
        public ClassesMappings()
        {
            CreateMap<Class, Class>().ForMember(x => x.ClassId, opt => opt.Ignore());
        }
    }
}
