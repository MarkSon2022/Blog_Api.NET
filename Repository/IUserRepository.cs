using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public User GetById(string id);
        public User GetByUsername(string username);
        public User GetByEmail(string email);
        public User GetByUsernameAndPassword(string username, string password);
        public User? ActivateUser(User user, Boolean status);
        public User Add(User user);
        public void Remove(User user);
        public User? Update(User user);
    }
}
