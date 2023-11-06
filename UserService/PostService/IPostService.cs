

namespace Backend.UserService
{
    public interface IPostService
    {
       Task<ServiceResponse<List<GetPostDTO>>> GetPosts(int userId);

       Task<ServiceResponse<List<GetPostDTO>>> GetAllPosts();


       Task<ServiceResponse<GetPostDTO>> GetPost(int id);

       Task<ServiceResponse<int>> AddPost(AddPostDTO post, int userId);

       Task<ServiceResponse<GetPostDTO>> UpdatePost(UpdatePostDTO updatedPost, int postId, int userId);

       Task<ServiceResponse<GetPostDTO>> DeletePost(int postId, int userId);

    }

}