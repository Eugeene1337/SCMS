﻿using System.ComponentModel.DataAnnotations;

namespace SCMS.API.DTO
{
    public class CreatePacketDto
    {
        [Required]
        public string PacketName { get; set; }
        [Required]
        public double PacketPrice { get; set; }
    }
}
