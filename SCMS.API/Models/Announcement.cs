using System;
using System.ComponentModel.DataAnnotations;

namespace SCMS.API.Models
{
    public class Announcement
    {
        [Key]
        public int AnnouncementId { get; set; }

        public string Text { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
