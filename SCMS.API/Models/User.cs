using Microsoft.AspNetCore.Identity;
using System;

namespace SCMS.API.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public string PostCode { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressNumber { get; set; }

        public string Role { get; set; }
    }
}
