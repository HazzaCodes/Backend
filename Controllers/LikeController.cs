using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class LikeController : Controller
    {
        private IlikeService likeService;
        public LikeController(IlikeService likeService)
        {
            this.likeService = likeService;
        }

        
      
      
    }
}