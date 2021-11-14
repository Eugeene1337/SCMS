using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SCMS.API.Models
{
    public class Packet
    {
        [Key]
        [JsonIgnore]
        public int PacketId { get; set; }
        public string PacketName { get; set; }
        public double PacketPrice { get; set; }
    }
}
