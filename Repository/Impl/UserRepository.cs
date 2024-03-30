using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        public User? ActivateUser(User user, bool status)=>UserDAO.Instance.ActivateUser(user, status); 

        public User Add(User user)=>UserDAO.Instance.Add(user);

        public IEnumerable<User> GetAll()=>UserDAO.Instance.GetAll();

        public User GetByEmail(string email)=>UserDAO.Instance.GetByEmail(email);   

        public User GetById(string id)=>UserDAO.Instance.GetById(id);   

        public User GetByUsername(string username)=>UserDAO.Instance.GetByUsername(username);   

        public User GetByUsernameAndPassword(string username, string password)=>UserDAO.Instance.GetByUsernameAndPassword(username, password);

        public void Remove(User user)=>UserDAO.Instance.Remove(user);

        public User? Update(User user)=>UserDAO.Instance.Update(user);

    }
}
