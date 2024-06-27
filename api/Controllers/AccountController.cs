using api.Dto.Account;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var foundUser = await _userManager.FindByNameAsync(loginDto.UserName);
            var foundUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
            if (foundUser != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(foundUser, loginDto.Password, false);
                if (!result.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        UserName = foundUser.UserName,
                        Email = foundUser.Email,
                        Token = _tokenService.CreateToken(foundUser)
                    });
                }
                else
                {
                    return Unauthorized("Password is incorrect");
                }
            }
            else
            {
                return Unauthorized("Invalid username");
            }
        }




        [HttpPost("register")]
        public async Task<IActionResult> ResisterUser([FromBody] UpdateAccountDto accountDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var theUser = new AppUser
                {
                    UserName = accountDto.UserName,
                    Email = accountDto.EmailAddress
                };

                var addedUser = await _userManager.CreateAsync(theUser, accountDto.Password);

                if (addedUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(theUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = theUser.UserName,
                            Email = theUser.Email,
                            Token = _tokenService.CreateToken(theUser)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, addedUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}