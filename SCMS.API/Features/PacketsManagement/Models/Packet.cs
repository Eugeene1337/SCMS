using System;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.Features.PacketManagement.Models
{
    public class Packet
    {
        [Key]
        public int PacketId { get; set; }
        public string PacketName { get; set; }
        public Decimal PacketPrice { get; set; }
    }
}
