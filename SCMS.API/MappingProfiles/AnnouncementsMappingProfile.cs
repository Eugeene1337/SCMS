using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class AnnouncementsMappingProfile : Profile
    {
        public AnnouncementsMappingProfile()
        {
            CreateMap<Announcement, Announcement>().ForMember(x => x.AnnouncementId, opt => opt.Ignore());
            CreateMap<Announcement, UpdateAnnouncementDto>().ReverseMap();
            CreateMap<Announcement, CreateAnnouncementDto>().ReverseMap();
        }
    }
}
