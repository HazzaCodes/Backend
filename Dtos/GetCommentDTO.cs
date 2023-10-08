using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class GetCommentDTO
    {
        public string Content { get; set; } = string.Empty;
        public DateTime DateCommented { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public int Likes { get; set; } // Number of likes
        public int Dislikes { get; set; } // Number of dislikes
    }
}