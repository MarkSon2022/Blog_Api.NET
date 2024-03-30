using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class PostRepository : IPostRepository
    {
        public Post Add(Post post)=>PostDAO.Instance.Add(post);
        
        public IEnumerable<Post> GetAll()=>PostDAO.Instance.GetAll();   

        public Post GetById(int id)=>PostDAO.Instance.GetById(id);

        public void Remove(Post post)=>PostDAO.Instance.Remove(post);

        public Post Update(Post post)=>PostDAO.Instance.Update(post);
    
    }
}
