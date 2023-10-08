using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend.UserService;
using System.Security.Cryptography.X509Certificates;

namespace Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PostController : Controller
    {
      
      private readonly IPostService postService;
      public PostController(IPostService postService)
      {
        this.postService = postService;
      }
      [HttpGet("Get posts")]
      public async Task<ActionResult<ServiceResponse<List<GetPostDTO>>>> GetPosts() {
        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var serviceResponse = await postService.GetPosts(userId);

        if (serviceResponse.Success) {
          return Ok(serviceResponse);
        }
        return BadRequest(serviceResponse);

      }

      [HttpGet("Get post details{postId}")]
      public async Task<ActionResult<ServiceResponse<GetPostDTO>>> GetPost(int postId) {

       var serviceResponse = await postService.GetPost(postId);
       if (serviceResponse.Success) {
        return Ok(serviceResponse);
       }
       return BadRequest(serviceResponse);
      }

      [HttpPost("Add post")]
      public async Task<ActionResult<ServiceResponse<GetPostDTO>>> AddPost(AddPostDTO newPost) {
         int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            if (newPost is null) {
            return BadRequest();
          }
         var serviceResponse = await postService.AddPost(newPost, userId);

    


         if (serviceResponse.Success) {
          return Ok(serviceResponse);
         }
         return BadRequest(serviceResponse);
      }
      [HttpPut("Update post/{id}")]
      public async Task<ActionResult<ServiceResponse<GetPostDTO>>> UpdatePost(UpdatePostDTO updatedPost) {
        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var serviceResponse = await postService.UpdatePost(updatedPost, userId);
        if (serviceResponse.Success) {
          return Ok(serviceResponse);
        }
        return BadRequest(serviceResponse);
        
      }
      
      [HttpDelete("Delete post/{postId}")]
      public async Task<ActionResult<ServiceResponse<GetPostDTO>>> DeletePost(int postId) {
        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

        var serviceResponse = await postService.DeletePost(postId, userId);
        if (serviceResponse.Success) {
          return Ok(serviceResponse);
        }
        return BadRequest(serviceResponse);

      }
    }
}