

using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Post
    {

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Likes { get; set; } // Number of likes
    public int Dislikes { get; set; } // Number of dislikes
    public DateTime DatePosted { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow; // Add this property
    public List<Comment> Comments{get;set;}
    public int UserId{get; set;}

    public string PostedBy {get; set;} = string.Empty;
    public User User { get; set; }

}

        
    }

