
using Backend.UserService.CommentService;

namespace Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]

    public class CommentController : Controller
    {
        
        private ICommentService  commentService;
        public CommentController(ICommentService commentService) {
            this.commentService = commentService;
        }
        
            
        
        [HttpPost("Add comment/{PostId}")]
        public async Task<ActionResult<ServiceResponse<GetCommentDTO>>> AddComent(AddCommentDTO commentDTO) {
             int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
             var serviceResponse = await commentService.AddComment(commentDTO, userId);
             if (serviceResponse.Success) {
                return Ok(serviceResponse);
             }
             return BadRequest(serviceResponse);
        }

        [HttpGet("Get comments/{postId}")]
        public async Task<ActionResult<ServiceResponse<List<GetCommentDTO>>>> GetComments(int postId) {
            var serviceResponse = await commentService.GetComments(postId);
            if (serviceResponse.Success) {
                return Ok(serviceResponse);
            }
            return BadRequest(serviceResponse);
        }


        [HttpPut("Update comment {PostId}/{CommentId}")]
         public async Task<ActionResult<ServiceResponse<GetCommentDTO>>> UpdateComent(UpdateCommentDTO newComment) {
             int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
             var serviceResponse = await commentService.UpdateComment(newComment, userId);
             if (serviceResponse.Success) {
                return Ok(serviceResponse);
             }
             return BadRequest(serviceResponse);
         }

         [HttpDelete("Delete comment/{PostId}/{CommentId}")]
             public async Task<ActionResult<ServiceResponse<GetCommentDTO>>> DeleteComent(DeleteCommentDTO deletedComment) {
             int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
             var serviceResponse = await commentService.DeleteComment(deletedComment, userId);
             if (serviceResponse.Success) {
                return Ok(serviceResponse);
             }
             return BadRequest(serviceResponse);
        }
        
      
}
}
