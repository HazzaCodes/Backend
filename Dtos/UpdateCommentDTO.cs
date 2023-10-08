using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class UpdateCommentDTO
    {
        public string Content { get; set; } = string.Empty;

        public int CommentId{get; set;}

        public int PostId{get; set;}
        
    }
}