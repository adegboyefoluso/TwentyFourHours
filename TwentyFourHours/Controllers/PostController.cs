using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwentyFourHour.Model;
using TwentyFourHour.Service;

namespace TwentyFourHours.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        // Authorization and method access
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var PostService = new PostService(userId);
            return PostService;
        }

        // POST
        public IHttpActionResult Post(PostCreate post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PostService service = CreatePostService();

            if (!service.CreatePost(post))
                return InternalServerError();

            return Ok();
        }

        // GET
        public IHttpActionResult Get()
        {
            
            PostService postService = CreatePostService();
            var posts = postService.GetPosts();
            return Ok(posts);
        }

        // GET by ID
        public IHttpActionResult Get(int id)
        {

            PostService postService = CreatePostService();
            var post = postService.GetPostById(id);
            return Ok(post);
        }


        // PUT

        public IHttpActionResult Put(PostEdit post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePostService();

            if (!service.UpdatePost(post))
                return InternalServerError();

            return Ok();
        }

        // DELETE
        public IHttpActionResult Delete(int id)
        {
            var service = CreatePostService();

            if (!service.DeletePost(id))
                return InternalServerError();

            return Ok();
        }
    }
}
