using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class CommentRepository : ICommentRepository
    {
        public Comment Add(Comment comment)=>CommentDAO.Instance.Add(comment);

        public IEnumerable<Comment> GetAll()=>CommentDAO.Instance.GetAll(); 

        public Comment GetById(int id)=>CommentDAO.Instance.GetById(id);

        public void Remove(Comment comment) => CommentDAO.Instance.Remove(comment);

        public Comment Update(Comment comment)=>CommentDAO.Instance.Update(comment);
        
    }
}
