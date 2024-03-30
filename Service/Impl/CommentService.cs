using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class CommentService : ICommentService
    {
        private ICommentRepository commentRepository;
        public CommentService (ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public Comment AddComment(Comment comment)
        {
            return commentRepository.Add(comment);
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return commentRepository.GetAll();
        }

        public Comment GetCommentById(int id)
        {
            return commentRepository.GetById(id);
        }

        public void RemoveComment(Comment comment)
        {
            commentRepository.Remove(comment);
        }

        public Comment UpdateComment(Comment comment)
        {
            return commentRepository.Update(comment);
        }
    }
}
