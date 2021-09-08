using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFourHour.Model
{
    public class PostDetail
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public List<PostCommentDetail> Comments { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
