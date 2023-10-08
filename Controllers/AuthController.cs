
namespace Backend.Controllers
{
    
    [Route("[controller]")]
    public class AuthController : Controller
    {

        
        private IAuthenticationService authenticationService;
        public AuthController(IAuthenticationService auth)
        {
            this.authenticationService = auth;
        }

        
        
        [HttpPost("Resgiter")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO user) {
            var serviceResponse = await authenticationService.Register(new User{Email = user.Email, Username = user.Username}, user.Password);
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