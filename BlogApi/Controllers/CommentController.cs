using BlogApi.Model;
using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Impl;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    [Authorize]
    public class CommentController : Controller
    {
        private ICommentService commentService;
        private IUserService userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            this.commentService = commentService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            return Ok(commentService.GetAllComments());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) { 
            if(id == null) {
                return BadRequest("You must enter the id");
            }
            var comment= commentService.GetCommentById(id);
            if (comment == null)
            {
                return BadRequest("There is no comment with this id: " + id);
            }
            else { 
                return Ok(comment);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                var deletedComment = commentService.GetCommentById(id);
                if (deletedComment != null)
                {
                    commentService.RemoveComment(deletedComment);
                    return Ok("Delete success!");
                }
                else
                {
                    return BadRequest("This category does not exist");
                }
            }
            else
            {
                return BadRequest("You must enter the id");
            }
        }

        [HttpPost]
        public IActionResult CreateComment(CommentDto commentDto) {
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.Id = commentDto.Id;
                comment.Content = commentDto.Content;
                comment.CreatedAt= DateTime.Now;
                comment.PostId = commentDto.PostId;
                comment.UserId = commentDto.UserId;

                var createdComment =commentService.AddComment(comment);
                return Ok(createdComment);
            }
            else
            {
                return BadRequest("You must enter valid comment");
            }
        }

        [HttpPut]
        public IActionResult UpdateComment(CommentDto commentDto)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.Id = commentDto.Id;
                comment.Content = commentDto.Content;
                comment.CreatedAt = DateTime.Now;
                comment.PostId = commentDto.PostId;
                comment.UserId = commentDto.UserId;

                var updatedComment = commentService.UpdateComment(comment);
                return Ok(updatedComment);
            }
            else
            {
                return BadRequest("You must enter valid comment");
            }
        }
    }
}
