using System;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.Models
{
    public class Subscription
    {
        [Key]
        public Guid SubscriptionId { get; set; }

        [Required]
        public int PacketId { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsActive { get; set; }

        public DateTime ValidTo { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
