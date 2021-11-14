using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SCMS.API.Models
{
    public class Class
    {
        [Key]
        [JsonIgnore]
        public int ClassId { get; set; }
        public int ActivityId { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public Guid TrainerUserId { get; set; }
    }
}
