using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFourHour.Model
{
    public class PostCommentDetail
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
