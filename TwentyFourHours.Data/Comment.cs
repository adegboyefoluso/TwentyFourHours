using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFourHours.Data
{
    public class Comment
    {

        [Key]
        public int CommentId { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public DateTimeOffset ModifiedUtc { get; set; }
        [Required]
        public virtual Post Post { get; set; }
        public virtual List<Like> Likes { get; set; }
        public int PostId { get; set; }
    }
}
