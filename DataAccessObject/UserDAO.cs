using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class UserDAO
    {
        private static UserDAO instance;
        private static readonly object locker = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        //METHOD

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                var context = new BlogContext();
                users = context.Users.AsQueryable().ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetById(string id)
        {
            var user = new User();
            try
            {
                var context = new BlogContext();
                user = context.Users.SingleOrDefault(u => u.Id.Equals(id));
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetByUsername(string username)
        {
            var user = new User();
            try
            {
                var context = new BlogContext();
                user = context.Users.SingleOrDefault(u => u.Username.Equals(username));
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetByEmail(string email)
        {
            var user = new User();
            try
            {
                var context = new BlogContext();
                user = context.Users.SingleOrDefault(u => u.Email.Equals(email));
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            var user = new User();
            try
            {
                var context = new BlogContext();
                user = context.Users.SingleOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
                if (user.Status.Equals(false)) {
                    return null;
                }else {
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public User Add(User user)
        {
            try
            {
                var existedUser = GetById(user.Id);
                if (existedUser == null)
                {
                    var context = new BlogContext();
                    context.Users.Add(user);
                    context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception("This user already exist");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(User user)
        {
            try
            {
                var existedUser = GetById(user.Id);
                if (existedUser != null)
                {
                    var context = new BlogContext();
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This user does not exist to delete");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public User? Update(User user)
        {
            try
            {
                var existedUser = GetById(user.Id);
                if (existedUser != null)
                {
                    var context = new BlogContext();
                    context.Entry<User>(user).State = EntityState.Modified;
                    context.SaveChanges();

                    return user;
                }
                else
                {
                    throw new Exception("This user does not exist to update");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public User? ActivateUser(User user, Boolean status)
        {
            try
            {
                var existedUser = GetById(user.Id);
                if (existedUser != null)
                {
                    var context = new BlogContext();
                    user.Status = status;
                    context.Entry<User>(user).State = EntityState.Modified;
                    context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception("This user does not exist to update");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //END METHOD
    }
}

