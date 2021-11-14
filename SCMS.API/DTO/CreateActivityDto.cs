using System.ComponentModel.DataAnnotations;

namespace SCMS.API.DTO
{
    public class CreateActivityDto
    {
        [Required]
        public string ActivityName { get; set; }
    }
}
