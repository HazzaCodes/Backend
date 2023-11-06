
namespace Backend.Controllers
{
      [ApiController]
      [Route("api/[controller]")]
        public class AuthController : Controller
    {
        private IAuthenticationService authenticationService;
        public AuthController(IAuthenticationService auth)
        {
            this.authenticationService = auth;
        }

        
        
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO user) {
            var serviceResponse = await authenticationService.Register(new User{Email = user.Email, Username = user.Username}, user.Password, user.ConfirmedPassword);
            if (serviceResponse.Success) {
                return Ok (serviceResponse);
            
            } 
            return BadRequest(serviceResponse);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDTO user) {
            var serviceResponse = await authenticationService.Login(user.Username, user.Password);
            if (serviceResponse.Success) {
                return Ok (serviceResponse);
            } 
            return BadRequest(serviceResponse);
        }
      
    }
}