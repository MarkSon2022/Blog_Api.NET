using BusinessObject;
using Repository;
using Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class PostService : IPostService
    {
        private IPostRepository repository;

        public PostService(IPostRepository repository)
        {
            this.repository = repository;
        }

        public Post AddPost(Post post)
        {

            return repository.Add(post);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return repository.GetAll();
        }

        public Post GetPostById(int id)
        {
            return repository.GetById(id);
        }

        public void RemovePost(Post post)
        {
            repository.Remove(post);
        }

        public Post UpdatePost(Post post)
        {
            return repository.Update(post);
        }
    }
}
