using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class ActivitiesMappingProfile : Profile
    {
        public ActivitiesMappingProfile()
        {
            CreateMap<Activity, Activity>().ForMember(x => x.ActivityId, opt => opt.Ignore());
            CreateMap<Activity, CreateActivityDto>().ReverseMap();
        }
    }
}
