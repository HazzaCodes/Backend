using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime DateCommented { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    // Define the foreign key for User
    public int  UserId { get; set; }
    public User User { get; set; }

    // Define the foreign key for Post
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int Likes { get; set; } // Number of likes
    public int Dislikes { get; set; } // Number of dislikes
}

}



