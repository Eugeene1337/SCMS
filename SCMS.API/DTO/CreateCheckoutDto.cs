using System.ComponentModel.DataAnnotations;

namespace SCMS.API.DTO
{
    public class CreateCheckoutDto
    {
        [Required]
        public int PackietId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
