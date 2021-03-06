using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string ActivityName { get; set; }
    }
}