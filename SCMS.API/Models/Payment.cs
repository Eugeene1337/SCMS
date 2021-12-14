using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public double PaymentAmount { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int PacketId { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
