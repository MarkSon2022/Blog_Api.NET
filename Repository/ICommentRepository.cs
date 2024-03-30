using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAll();
        public Comment GetById(int id);
        public Comment Add(Comment comment);
        public void Remove(Comment comment);

        public Comment Update(Comment comment);
    }
}
