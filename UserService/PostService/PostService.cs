using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Azure;
using Backend.Dtos;

namespace Backend.UserService
{
    public class PostService: IPostService


    {
        private DataContext context;
        private IMapper mapper;
        public PostService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            
        }

        public async Task<ServiceResponse<List<GetPostDTO>>> GetPosts(int userId) {
           var serviceResponse = new ServiceResponse<List<GetPostDTO>>();
           var user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            try {
            var posts = context.Posts.Where(post => post.UserId == userId).ToList();
            serviceResponse.Data = posts.Select(post => mapper.Map<GetPostDTO>(post)).ToList();
            } catch (Exception) {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to retrieve posts";
            }
            return serviceResponse;
           
    }

    public async Task<ServiceResponse<GetPostDTO>> GetPost(int id) {
        var serviceResponse = new ServiceResponse<GetPostDTO>();
        try {
        var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        serviceResponse.Data = mapper.Map<GetPostDTO>(post);
        serviceResponse.Message = "Post retrieved successfully";
        } catch(Exception) {
            serviceResponse.Success = false;
            serviceResponse.Message = "Failed to retrieve post";
        }
        return serviceResponse;
    }


    public async Task<ServiceResponse<int>> AddPost(AddPostDTO postDTO, int userId) {
        var serviceResponse = new ServiceResponse<int>();
        try {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var post = mapper.Map<Post>(postDTO);
        post.UserId = userId;
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        serviceResponse.Data = post.Id;
        } catch(Exception) {
            serviceResponse.Message = "Failed to add post";
            serviceResponse.Success = false;
        }
        return serviceResponse;
        
    }

    public async Task<ServiceResponse<GetPostDTO>> UpdatePost(UpdatePostDTO updatedPost, int userId) {
        var serviceResponse = new ServiceResponse<GetPostDTO>();
        int postId = updatedPost.Id;
        try {
        var post = context.Posts.FirstOrDefault(p => p.Id == postId && p.User.Id == userId);
        mapper.Map(updatedPost, post);
        await context.SaveChangesAsync();
        serviceResponse.Data = mapper.Map<GetPostDTO>(post);
        serviceResponse.Message = "Updated post successfully";
        } catch(Exception) {
            serviceResponse.Message = "Failed to update post";
            serviceResponse.Success = false;
        }
        return serviceResponse;
    }

        public async Task<ServiceResponse<GetPostDTO>> DeletePost(int postId, int userId)
        {
            var serviceResponse = new ServiceResponse<GetPostDTO>();

            try {
            var post= await context.Posts.FirstOrDefaultAsync(p => p.Id == postId && p.User.Id == userId);
            serviceResponse.Data = mapper.Map<GetPostDTO>(post);
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            serviceResponse.Message = "Post deleted successfully";
           
            } catch (Exception) {
                serviceResponse.Message = "Error post not found";
            }
             return serviceResponse;
        }
    
  
    }
    }
