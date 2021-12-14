using SCMS.API.DTO;
using SCMS.API.Models;
using System;
using System.Collections.Generic;

namespace SCMS.API.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User GetSingle(Guid id);
        List<User> GetAll();
        List<User> GetTrainers();
        void Update(User user);
        void Delete(Guid id);
        public bool Save();
    }
}
