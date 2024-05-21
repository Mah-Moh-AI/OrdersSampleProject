using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain.RepositoryContracts;
using Project.Core.DTO.AuthenticationDTO;
using Project.Infrastructure.Repositories;
using LoginRequest = Project.Core.DTO.AuthenticationDTO.LoginRequest;
using RegisterRequest = Project.Core.DTO.AuthenticationDTO.RegisterRequest;

namespace Project.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ILogger<AuthController> logger, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            logger.LogInformation("Registeration of User Name {user}", registerRequest.UserName);

            IdentityUser identityUser = new IdentityUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.UserName
            };

            IdentityResult identityResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            if (!identityResult.Succeeded)
            {
                logger.LogWarning("User {user} registeration failed. Errors: {errors}", registerRequest.UserName, string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                return BadRequest("Something went wrong");
            }

            if (registerRequest.Roles != null && registerRequest.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequest.Roles);
            }

            if (!identityResult.Succeeded)
            {
                logger.LogWarning("User {user} registeration failed. Errors: {errors}", registerRequest.UserName, string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                return BadRequest("Something went wrong");
            }

            logger.LogInformation("User name {user} is registered successfully", registerRequest.UserName);
            return Ok("User was registered! Please login");

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            logger.LogInformation("Logging in user {user} ...", loginRequest.UserName);

            IdentityUser? user = await userManager.FindByEmailAsync(loginRequest.UserName);

            if (user == null)
            {
                logger.LogWarning("User {user} login failed. User don't exist in database", loginRequest.UserName);
                return BadRequest("Username or password incorrect");
            }

            bool checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (!checkPasswordResult)
            {
                logger.LogWarning("User {user} login failed. Password is wrong", loginRequest.UserName);
                return BadRequest("Username or password incorrect");
            }

            IList<string> roles = await userManager.GetRolesAsync(user);

            if (roles == null || roles.Count == 0)
            {
                logger.LogWarning("User {user} login failed. User roles do not exist", loginRequest.UserName);
                return BadRequest("Something went wrong");
            }

            string jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

            LoginResponse loginResponse = new LoginResponse
            {
                JwtToken = jwtToken,
            };

            logger.LogInformation("User {user} is logged in successfully", loginRequest.UserName);
            return Ok(loginResponse);
        }

    }
}
