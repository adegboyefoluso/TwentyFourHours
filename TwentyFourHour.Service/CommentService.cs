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
                    Comment = model.Comment
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
                                    CommentId = c.CommentId,
                                    Comment = c.Comment,
                                    PostId = c.Post.Text
                                });
                return post.ToArray();
            }
        }
        public CommentDetails GetCommentByPostId(int id) // Get comments by PostId
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.PostId == id && e.AuthorId == _userId);
                return
                new CommentDetails
                {
                    CommentId = entity.Id,
                    Comment = entity.Comment,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }
        public CommentDetails GetCommentById(int id) // get comment by id
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Comments
                    .Single(e => e.CommentId == id && e.AuthorId == _userId);
                return
                    new CommentDetails
                    {
                        CommentId = entity.CommentId,
                        Comment = entity.Comment,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        PostId = entity.PostId
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
                    .Single(e => e.CommentId == model.CommentId && e.AuthorId == _userId);
                entity.CommentId = model.CommentId;
                entity.Comment = model.Comment;
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
                    .Single(e => e.CommentId == commentId && e.AuthorId == _userId);
                ctx.Comments.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}