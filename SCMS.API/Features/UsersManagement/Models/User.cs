using Microsoft.AspNetCore.Identity;
using System;

namespace SCMS.API.Features.Models
{
    public class User:IdentityUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
