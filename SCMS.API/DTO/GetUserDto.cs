using System;

namespace SCMS.API.DTO
{
    public class GetUserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string PostCode { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressNumber { get; set; }
        public string Role { get; set; }
    }
}
