using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SCMS.API.Models
{
    public class Packet
    {
        [Key]
        public int PacketId { get; set; }

        [Required]
        public string PacketName { get; set; }

        [Required]
        public double PacketPrice { get; set; }
    }
}
