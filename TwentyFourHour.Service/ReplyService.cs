using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwentyFourHour.Model.Reply;
using TwentyFourHours.Data;

namespace TwentyFourHour.Service
{
    public class ReplyService
    {
        private readonly Guid _UserId;
        public ReplyService(Guid userId)
        {
            _UserId = userId;
        }

        public bool CreateRepy(ReplyCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var comment = ctx
                                    .Comments
                                    .Find(model.CommentId);
                if (comment != null)
                {
                    var entity = new Reply()
                    {
                        Text = model.Text,
                        CommentId = model.CommentId,
                        CreatedUTc = DateTime.Now,
                        AuthorId = _UserId

                    };
                    comment.Replies.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }

        public ReplyCommentDetail GetReplyByCommentId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                                .Comments
                               .SingleOrDefault(e => e.Id == id);

                return new ReplyCommentDetail()
                {
                    CommentId = query.Id,
                    CreatedUtc = query.CreatedUtc,
                    ModifiedUtc = query.ModifiedUtc,
                    Text = query.Text,
                    Replies = ctx
                                    .Replies
                                    .Where(e => e.CommentId == id)
                                    .Select(e => new ReplyDetail()
                                    {
                                        ReplyId = e.Id,
                                        Text = e.Text,
                                        CreatedUTc = e.CreatedUTc,
                                        ModifiedUTc = e.ModifiedUTc
                                    }).ToList()


                };

            }
        }
        public List<ReplyDetail> GetReplyByAuthorId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                                    .Replies
                                    .Where(e => e.AuthorId == _UserId)
                                    .Select(e => new ReplyDetail()
                                    {
                                        ReplyId = e.Id,
                                        Text = e.Text,
                                        CreatedUTc = e.CreatedUTc,
                                        ModifiedUTc = e.ModifiedUTc
                                    }).ToList();

                return query;
            }
        }

        public bool EditReply(ReplyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var reply = ctx
                                .Replies
                                .SingleOrDefault(e => e.Id == model.Id & e.AuthorId == _UserId);
                if (reply is null) return false;
                reply.Text = model.Text;
                reply.CommentId = model.CommentId;
                reply.ModifiedUTc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;

            }
        }
        public bool DeleteReply(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var reply = ctx
                               .Replies
                               .SingleOrDefault(e => e.Id == id && e.AuthorId == _UserId);
                if (reply is null) return false;
                ctx.Replies.Remove(reply);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}

