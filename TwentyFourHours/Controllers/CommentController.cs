using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TwentyFourHour.Model;
using TwentyFourHour.Service;

namespace TwentyFourHours.Controllers
{
    public class CommentController : ApiController
    {
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.ToString());
            var commentService = new CommentService(userId);
            return commentService;
        }
        public IHttpActionResult Get()
        {
            CommentService commentService = CreateCommentService();
            var comments = commentService.GetAllComments();
            return Ok(comments);
        }
        public IHttpActionResult Post(CommentCreate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCommentService();

            if (!service.CreateComment(comment))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Put(CommentUpdate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var servic = CreateCommentService();

            if (!CreateCommentService().UpdateComment(comment))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateCommentService();
            if (!service.DeleteComment(id))
                    return InternalServerError();

            return Ok();
        }
    }
}