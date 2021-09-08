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
        public string Comment { get; set; }
        [Required]
        public virtual Post postid { get; set; }

    }
}
