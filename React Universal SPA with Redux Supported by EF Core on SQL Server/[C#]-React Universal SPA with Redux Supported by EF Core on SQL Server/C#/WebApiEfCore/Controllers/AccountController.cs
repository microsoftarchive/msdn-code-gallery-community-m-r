using System.Threading.Tasks;
using Auth.Models;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepo _userRepo;

        public AccountController(SignInManager<IdentityUser> signInManager, IUserRepo userRepo)
        {
            _signInManager = signInManager;
            _userRepo = userRepo;
        }

        // POST api/Account/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<UserDTO> Login(LoginDTO model)
        {
            if (User.Identity.IsAuthenticated) return _userRepo.GetUserByEmail(User.Identity.Name);

            if (!ModelState.IsValid) return new UserDTO
            {
                UserId = -1,
                ScreenName = "invalid"
            };

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, 
                model.RememberMe, model.LockoutFailure);

            if (result.Succeeded)
            {
                return _userRepo.GetUserByEmail(model.Email);
            }

            return result.IsLockedOut ? new UserDTO
                {
                    UserId = -1,
                    ScreenName = "lockout"
                }
                : new UserDTO
                {
                    UserId = -1,
                    ScreenName = "false"
                };

        }

        // GET api/Account/checkLoginStatus
        [HttpGet("checkLoginStatus")]
        [AllowAnonymous]
        public UserDTO CheckLoginStatus()
        {
            return User.Identity.IsAuthenticated
                ? _userRepo.GetUserByEmail(User.Identity.Name)
                : new UserDTO
                {
                    UserId = -2,
                    ScreenName = "not loggedIn"
                };
        }

        // POST api/Account/logout
        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<bool> Logout()
        {
            if (User.Identity.IsAuthenticated) await _signInManager.SignOutAsync();

            return true;
        }

    }
}
