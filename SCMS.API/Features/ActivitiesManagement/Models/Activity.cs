using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.Features.ActivitiesManagement.Models
{
    public class Activity
    {
        [Key]
        [JsonIgnore]
        public int ActivityId { get; set; }
        [Required]
        public string ActivityName { get; set; }
    }
}
