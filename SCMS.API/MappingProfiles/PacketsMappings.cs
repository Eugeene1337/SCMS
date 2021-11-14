using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class PacketsMappings : Profile
    {
        public PacketsMappings()
        {
            CreateMap<Packet, Packet>().ForMember(x => x.PacketId, opt => opt.Ignore());
            CreateMap<Activity, CreatePacketDto>().ReverseMap();
        }
    }
}
