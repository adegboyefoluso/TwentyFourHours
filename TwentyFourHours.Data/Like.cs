using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFourHours.Data
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public Guid OwnerId { get; set; }
    }
}
