using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwentyFourHour.Model.Reply;
using TwentyFourHour.Service;

namespace TwentyFourHours.Controllers
{
    [Authorize]
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyServices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new ReplyService(userid);
            return service;
        }

        [HttpPost]
        public IHttpActionResult Post(ReplyCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateReplyServices();
            var reply = service.CreateRepy(model);
            if (reply)
            {
                return Ok("Reply Created succesfuly");
            }
            return InternalServerError();

        }

        [HttpGet]
        public IHttpActionResult GetReplyByIdCommentId(int id)
        {
            var service = CreateReplyServices();
            var reply = service.GetReplyByCommentId(id);
            return Ok(reply);

        }


        [HttpGet]
        public IHttpActionResult GetReplyByAuthor()
        {
            var service = CreateReplyServices();
            var reply = service.GetReplyByAuthorId();
            return Ok(reply);

        }

        [HttpDelete]
        public IHttpActionResult DeleteReply(int id)
        {
            var service = CreateReplyServices();
            var reply = service.DeleteReply(id);
            if (reply is false) return InternalServerError();

            return Ok("Reply deleted succesfully");

        }
        [HttpPut]
        public IHttpActionResult Editreply(ReplyEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateReplyServices();
            var reply = service.EditReply(model);
            if (reply is true) return Ok("reply Updated succesfully");
            return InternalServerError();

        }

    }
}

