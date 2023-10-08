using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace Backend.Dtos
{
    public class AddCommentDTO
    {
         public string Content { get; set; } = string.Empty;
         public int PostId { get; set; }

    }
}