using System;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.DTO
{
    public class CreateClassDto
    {
        [Required]
        public int ActivityId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public Guid TrainerUserId { get; set; }
    }
}
