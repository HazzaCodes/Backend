using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.UserService.CommentService
{
    public interface ICommentService
    {
     Task<ServiceResponse<List<GetCommentDTO>>> GetComments(int postId);
     Task<ServiceResponse<GetCommentDTO>> AddComment(int postId, string newComment,  int commenterId);
     Task<ServiceResponse<GetCommentDTO>> UpdateComment(int postId, int commentId, string newComment, int UserId);
     Task<ServiceResponse<GetCommentDTO>> DeleteComment(DeleteCommentDTO deletedComment, int commenterId);


    }
}