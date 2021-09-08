using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwentyFourHour.Model;
using TwentyFourHours.Data;

namespace TwentyFourHour.Service
{
    public class CommentService
    {
        private readonly Guid _userId;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateComment(CommentCreate model) //Post comment
        {
            var entity =
                new Comment()
                {
                    AuthorId = _userId,
                    //Id = model.Id,
                    Text = model.Comment
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CommentListItem> GetCommentByAuthorId()  // get comment by Author id
        {
            using (var ctx = new ApplicationDbContext())
            {
                var post = ctx
                                .Comments
                                .Where(c => c.AuthorId == _userId).
                                Select(c =>
                                new CommentListItem
                                {
                                    CommentId = c.Id,
                                    Comment = c.Text,
                                    Post = c.Post.Text
                                });
                return post.ToArray();
            }
        }
        public CommentContents GetCommentByPostId(int id) // Get comments by PostId
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.PostId == id && e.AuthorId == _userId);
                return
                new CommentContents
                {
                    CommentId = entity.Id,
                    Text = entity.Text,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }
        public bool UpdateComment(CommentUpdate model) // Update comment
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == model.CommentId && e.AuthorId == _userId);
                entity.Id = model.CommentId;
                entity.Text = model.Text;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteComment(int commentId) // delete comment
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == commentId && e.AuthorId == _userId);
                ctx.Comments.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}