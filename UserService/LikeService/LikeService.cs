using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.UserService
{
    public class LikeService: IlikeService
    {
         private DataContext context;
        private IMapper mapper;
        public LikeService(DataContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }
        
           public async Task<ServiceResponse<GetCommentDTO>> LikeComment(LikeCommentDTO likedComment) {
            var serviceResponse = new ServiceResponse<GetCommentDTO>();
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == likedComment.Id &&
            c.PostId ==likedComment.PostId);
            int updatedLikes = comment.Likes + 1;
            comment.Likes = updatedLikes;
            await context.SaveChangesAsync();
            serviceResponse.Data = mapper.Map<GetCommentDTO>(comment);
            return serviceResponse;
        }

         public async Task<ServiceResponse<GetCommentDTO>> DislikeComment(LikeCommentDTO dislikedComment) {
            var serviceResponse = new ServiceResponse<GetCommentDTO>();
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == dislikedComment.Id &&
            c.PostId ==dislikedComment.PostId);
            int updatedDislikes = comment.Likes + 1;
            comment.Dislikes = updatedDislikes;
            await context.SaveChangesAsync();
            serviceResponse.Data = mapper.Map<GetCommentDTO>(comment);
            return serviceResponse;
        }

        
    }
}
