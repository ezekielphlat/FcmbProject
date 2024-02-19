using FcmbInterview.Application.Common.Interfaces.Persistence;
using FcmbInterview.Application.Common.Interfaces.Persistence.Common;
using FcmbInterview.Contract.Airtime;
using FcmbInterview.Contract.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FcmbInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       // [SwaggerOperation(Summary = "Creates new user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpPost]
        [Route("addUser")]
        public async Task<ActionResult> PostUser([FromBody] AddUserRequest request)
        {
          
           var result =  await _userRepository.AddUser(request.userName, request.UserType.ToUpper());

            return StatusCode((int)result.StatusCode, result);

        }

       // [SwaggerOperation(Summary = "gets list of users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpGet]
        [Route("alluser")]
        public async Task<ActionResult> GetUser()
        {

            var result = await _userRepository.GetAllUser();

            return StatusCode((int)result.StatusCode, result);

        }

        //[SwaggerOperation(Summary = "Get single user by userName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [HttpGet]
        [Route("{userName}")]       
        public async Task<ActionResult> GetUserByUserName(string userName)
        {

            var result = await _userRepository.GetUserByUserName(userName);

            return StatusCode((int)result.StatusCode, result);

        }
    }
    
}
