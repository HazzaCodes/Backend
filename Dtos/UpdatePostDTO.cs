using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class UpdatePostDTO
    {
        public int Id {get; set;}
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        
    }
}