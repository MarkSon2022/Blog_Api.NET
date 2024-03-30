using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public User? ActivateUser(User user, Boolean status);
        public User? GetById(string id);
        public User? GetByUsername(string username);
        public User? GetByEmail(string email);
        public User? GetByUsernameAndPassword(string username, string password);
        public User? Add(User user);
        public void Remove(User user);
        public User? Update(User user);
    }
}
