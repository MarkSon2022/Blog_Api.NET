using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CommentDAO
    {
        private static CommentDAO instance;
        private static readonly object locker=new object();
        private CommentDAO() { }    
        public static CommentDAO Instance {
            get {
                lock (locker) { 
                    if (instance == null)
                    {
                        instance = new CommentDAO();
                    }
                    return instance;
                }            
            } 
        }


        //METHOD

        public IEnumerable<Comment> GetAll()
        {
            try
            {
                List<Comment> comments = new List<Comment>();
                var context = new BlogContext();
                comments = context.Comments.ToList();
                
                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Comment GetById(int id)
        {
            try
            {
                var comment = new Comment();    
                var context = new BlogContext();
                comment = context.Comments.SingleOrDefault(c=>c.Id.Equals(id));
                
                return comment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Comment Add(Comment comment)
        {
            try { 
                var existedComment= GetById(comment.Id);
                if (existedComment == null) { 
                    var context= new BlogContext();
                    context.Comments.Add(comment);
                    context.SaveChanges();
                    return comment;
                }
                else
                {
                    throw new Exception("This comment already exists!");
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Remove(Comment comment)
        {
            try
            {
                var existedComment = GetById(comment.Id);
                if (existedComment != null)
                {
                    var context = new BlogContext();
                    context.Comments.Remove(comment);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This comment does not exist to delete!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Comment Update(Comment comment)
        {
            try
            {
                var existedComment = GetById(comment.Id);
                if (existedComment != null)
                {
                    var context = new BlogContext();
                    context.Entry<Comment>(comment).State = EntityState.Modified;   
                    context.SaveChanges();
                    return comment;
                }
                else
                {
                    throw new Exception("This comment does not exist to delete!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
