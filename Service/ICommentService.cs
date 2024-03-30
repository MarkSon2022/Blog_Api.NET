using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICommentService
    {
        public IEnumerable<Comment> GetAllComments();
        public Comment GetCommentById(int id);
        public Comment AddComment(Comment comment);
        public void RemoveComment(Comment comment);
        public Comment UpdateComment(Comment comment);
    }
}
