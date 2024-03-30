using BlogApi.Model;
using BusinessObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Service;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "staff")]
    public class PostController : Controller
    {
        private IPostService postService;
        private ICategoryService categoryService;
        private IUserService userService;
        public PostController(IPostService postService, ICategoryService categoryService, IUserService userService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "staff,user")]
        public IActionResult GetAll()
        {
            return Ok(postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "staff")]
        public IActionResult GetPostById(int id)
        {
            if (id == -1)
            {
                return BadRequest("Id must not be empty");
            }
            var post = postService.GetPostById(id);

            if (post == null)
            {
                return BadRequest("There is no post with id: " + id);
            }
            else
            {
                return Ok(post);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "staff")]
        public IActionResult DeletePostById(int id)
        {
            var deletedPost = postService.GetPostById(id);
            if (deletedPost != null)
            {
                postService.RemovePost(deletedPost);
                return Ok("Delete success!");
            }
            else
            {
                return BadRequest("No Post to delete !");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "staff")]
        public IActionResult CreatePost([FromBody] PostDto postDto)
        {
            if (ModelState.IsValid)
            {
                var post = new Post();
                post.Id = postDto.Id;
                post.Title = postDto.Title;
                post.AuthorId = postDto.AuthorId;
                post.CategoryId = postDto.CategoryId;
                post.Content = postDto.Content;
                post.PublishedAt = DateTime.Now;

                var createdPost = postService.AddPost(post);
                return new CreatedAtActionResult(nameof(createdPost), "Post", new { id = post.Id }, post);
            }
            else
            {
                return BadRequest("You must enter valid post");
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "staff")]
        public IActionResult UpdatePost([FromBody] PostDto postDto)
        {
            if (ModelState.IsValid)
            {
                var post = new Post();
                post.Id = postDto.Id;
                post.Title = postDto.Title;
                post.AuthorId = postDto.AuthorId;
                post.CategoryId = postDto.CategoryId;
                post.Content = postDto.Content;
                post.PublishedAt = DateTime.Now;

                var updatePost = postService.UpdatePost(post);
                return Ok(updatePost);
            }
            else
            {
                return BadRequest("You must enter valid post");
            }
        }

    }
}
