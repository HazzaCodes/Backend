using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Backend.Dtos;

namespace Backend
{
    public class AutoMapperProfile : Profile
    {
        public  AutoMapperProfile() 
        {
             CreateMap<Post, GetPostDTO>();
             CreateMap<Comment, GetCommentDTO>();
             CreateMap<AddPostDTO, Post>();
             CreateMap<UpdatePostDTO, Post>();
        }
    }
}