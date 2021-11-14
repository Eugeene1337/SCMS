using AutoMapper;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class ActivitiesMappings : Profile
    {
        public ActivitiesMappings()
        {
            CreateMap<Activity, Activity>().ForMember(x => x.ActivityId, opt => opt.Ignore());
        }
    }
}
