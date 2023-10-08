using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class GetPostDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Likes { get; set; } // Number of likes
        public int Dislikes { get; set; } // Number of dislikes
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow; // Add this property

    }
}