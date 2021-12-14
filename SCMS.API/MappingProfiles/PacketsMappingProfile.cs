using AutoMapper;
using SCMS.API.DTO;
using SCMS.API.Models;

namespace SCMS.API.MappingProfiles
{
    public class PacketsMappingProfile : Profile
    {
        public PacketsMappingProfile()
        {
            CreateMap<Packet, Packet>().ForMember(x => x.PacketId, opt => opt.Ignore());
            CreateMap<Packet, CreatePacketDto>().ReverseMap();
        }
    }
}
