using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.UserService
{
    public interface IlikeService
    {
        Task<ServiceResponse<GetCommentDTO>>LikeComment(LikeCommentDTO likedComment);
        Task<ServiceResponse<GetCommentDTO>>DislikeComment(LikeCommentDTO likedComment);



        
    }
}