using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPostRepository
    {
        public IEnumerable<Post> GetAll();
        public Post GetById(int id);
        public Post Add(Post post);
        public void Remove(Post post);
        public Post Update(Post post);
    }
}
