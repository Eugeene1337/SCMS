using System;

namespace SCMS.API.DTO
{
    public class GetClassDto
    {
        public int ClassId { get; set; }

        public int ActivityId { get; set; }

        public DateTime DateTime { get; set; }

        public int Duration { get; set; }

        public Guid TrainerUserId { get; set; }

        public string TrainerName { get; set; }

        public string TrainerSurname { get; set; }
    }
}
