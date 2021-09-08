using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFourHour.Model
{
    public class CommentListItem
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Text { get; set; }
        [Display(Name ="Created")]
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
