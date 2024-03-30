using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPostService
    {
        public IEnumerable<Post> GetAllPosts();
        public Post GetPostById(int id);
        public Post AddPost(Post post);
        public void RemovePost(Post post);
        public Post UpdatePost(Post post);
    }
}
