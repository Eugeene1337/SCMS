using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SCMS.API.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int Duration { get; set; }

        public string TrainerUserId { get; set; }
    }
}