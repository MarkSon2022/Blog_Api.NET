using BusinessObject;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? ActivateUser(User user, bool status)
        {
            return _userRepository.ActivateUser(user, status);
        }

        public User? Add(User user)
        {
            return _userRepository.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User? GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public User? GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public User? GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public User? GetByUsernameAndPassword(string username, string password)
        {
            return _userRepository.GetByUsernameAndPassword(username, password);
        }

        public void Remove(User user)
        {
            _userRepository.Remove(user);
        }

        public User? Update(User user)
        {
            return _userRepository.Update(user);
        }
    }
}
