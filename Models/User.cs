using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {       
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;

            public DateTime DateCreated { get; set; } = DateTime.UtcNow;
            public byte[] PasswordHash{get; set;} = new byte[0];

            public byte[] PasswordSalt{get; set;} = new byte[0];

            public List<Post> UserPosts{get; set;}

            public List<Comment> UserComments{get; set;}
        }


    }
