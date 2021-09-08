using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFourHour.Model
{
    public class CommentDetails
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
        public int PostId { get; set; }
    }
}
