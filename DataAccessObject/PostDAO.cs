using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class PostDAO
    {
        private static PostDAO instance;    
        private static readonly object instanceLock = new object(); 
        private PostDAO() { }
        public static PostDAO Instance { 
            get { 
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostDAO();
                    }
                    return instance;
                }
            } 
        }

        //METHOD
        public IEnumerable<Post> GetAll() {
            try
            {
                List<Post> posts = new List<Post>();
                var context= new BlogContext();
                posts= context.Posts.Include(p=>p.Author).Include(p => p.Category).AsQueryable().ToList();
                
                return posts;
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public Post GetById(int id)
        {
            try
            {
                Post post = new Post();
                var context = new BlogContext();
                post = context.Posts.Include(p => p.Author).Include(p=>p.Category).SingleOrDefault(p=>p.Id.Equals(id));
                
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Post Add(Post post)
        {
            try
            {
                var existedPost = GetById(post.Id);
                if (existedPost == null)
                {
                    var context = new BlogContext();
                    context.Posts.Add(post);
                    context.SaveChanges();
                    return post;
                }
                else
                {
                    throw new Exception("This post already exist");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Post post)
        {
            try
            {
                var existedPost = GetById(post.Id);
                if (existedPost != null)
                {
                    var context = new BlogContext();
                    context.Posts.Remove(post);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This post does not exist to remove");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Post Update(Post post)
        {
            try
            {
                var existedPost = GetById(post.Id);
                if (existedPost != null)
                {
                    var context = new BlogContext();
                    context.Entry<Post>(post).State = EntityState.Modified;
                    context.SaveChanges();
                    return post;
                }
                else
                {
                    throw new Exception("This post does not exist to delete");
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
