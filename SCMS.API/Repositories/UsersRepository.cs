using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetSingle(Guid id) => _context.Users.Find(id.ToString());

        public List<User> GetAll() => _context.Users.ToList();

        public void Update(User user)
        {
            _context.Update(user);
        }

        public void Delete(Guid id)
        {
            var user = GetSingle(id);

            if(user != null)
            {
                _context.Remove(user);
            }
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public List<User> GetTrainers()
        {
            var users = GetAll();
            var trainers = from user in users
                           where user.Role == UserRoles.Employee
                              select user;

            return trainers.ToList();
        }
    }
}
