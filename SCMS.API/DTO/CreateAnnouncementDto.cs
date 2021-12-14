using System.ComponentModel.DataAnnotations;

namespace SCMS.API.DTO
{
    public class CreateAnnouncementDto
    {
        public string Text { get; set; }

        [MinLength(20, ErrorMessage = "Shoud be GUID")]
        public string CreatedBy { get; set; }
    }
}
