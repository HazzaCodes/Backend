using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper.Execution;

namespace Backend.UserService.CommentService
{
    public class CommentService : ICommentService
    {
        private DataContext context;
        private IMapper mapper;
        public CommentService(DataContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<GetCommentDTO>> AddComment(int postId, string newComment,  int commenterId)
        {
            var serviceResponse = new ServiceResponse<GetCommentDTO>();
        
            try{
                var post = await context.Posts.FirstOrDefaultAsync(p => p.Id  == postId);
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == commenterId);

                if (post is null) {
                    serviceResponse.Message = "post is null not found can't add to post " + postId;
                    return serviceResponse;
                }
                var comment = new Comment{PostId = postId, Content = newComment, Post = post, UserId = commenterId, Commenter = user.Username};
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
                serviceResponse.Data = mapper.Map<GetCommentDTO>(comment);
                serviceResponse.Message = "Comment added successfully";
            } catch (Exception) {
                serviceResponse.Message =  "post id = " + postId + " commenter id = " + commenterId;
            }
            
     
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCommentDTO>> DeleteComment(DeleteCommentDTO deleteComment, int commenterId)
        {
            var serviceResponse = new ServiceResponse<GetCommentDTO>();

            try
            {
                var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == deleteComment.CommentId
                && c.PostId ==deleteComment.PostId && c.UserId == commenterId);
                serviceResponse.Data = mapper.Map<GetCommentDTO>(comment);
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
                serviceResponse.Message = "Comment deleted successfully";
            }
            catch (Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to delete comment";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCommentDTO>>> GetComments(int postId)

        {
            var serviceResponse = new ServiceResponse<List<GetCommentDTO>>();

            try {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            var Comments = context.Comments.Where(c => c.Post.Id == postId).ToList();
            List<GetCommentDTO> commentsDTO = Comments.Select(c => mapper.Map<GetCommentDTO>(c)).ToList();
            serviceResponse.Data = commentsDTO;
            } catch (Exception) {
                serviceResponse.Success = false;
                serviceResponse.Message = "failed to retrive posts";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCommentDTO>> UpdateComment(int postId, int commentId, string newComment, int userId)
        {
            var serviceResponse = new ServiceResponse<GetCommentDTO>();

            try
            {
                var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId&&
                c.PostId == postId && c.UserId == userId);
                mapper.Map(newComment, comment);
                comment.LastUpdated = DateTime.UtcNow;
                await context.SaveChangesAsync();
                serviceResponse.Message = "Updated  comment successfully";
                serviceResponse.Data = mapper.Map<GetCommentDTO>(comment);
            }
            catch (Exception)
            {
                serviceResponse.Message = "Failed to update comment";
                serviceResponse.Success = false;
            }
            return serviceResponse;

        }
    }
}