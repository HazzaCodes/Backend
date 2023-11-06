
using Backend.UserService.CommentService;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class CommentController : Controller
    {
        
        private ICommentService  commentService;
        public CommentController(ICommentService commentService) {
            this.commentService = commentService;
        }
        
            
        
        [HttpPost("AddComment/{postId}")]
        public async Task<ActionResult<ServiceResponse<GetCommentDTO>>> AddComent(int postId,[FromBody] string content) {
             int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
             var serviceResponse = await commentService.AddComment(postId, content, userId);
             if (serviceResponse.Success) {
                return Ok(serviceResponse);
             }
             return BadRequest(serviceResponse);
        }

        [HttpGet("GetComments/{postId}")]
        public async Task<ActionResult<ServiceResponse<List<GetCommentDTO>>>> GetComments(int postId) {
            var serviceResponse = await commentService.GetComments(postId);
            if (serviceResponse.Success) {
                return Ok(serviceResponse);
            }
            return BadRequest(serviceResponse);
        }


        [HttpPut("UpdateComment {postId}/{commentId}")]
         public async Task<ActionResult<ServiceResponse<GetCommentDTO>>> UpdateComment(int postId, int commentId, string content) {
             int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
             var serviceResponse = await commentService.UpdateComment(postId, commentId, content, userId);
             if (serviceResponse.Success) {
                return Ok(serviceResponse);
             }
             return BadRequest(serviceResponse);
         }

         [HttpDelete("DeleteComment/{PostId}/{CommentId}")]
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
