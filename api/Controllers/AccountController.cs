using api.Dto;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> ResisterUser([FromBody] UpdateAccountDto accountDto)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest("This is a bad request");
                }

                var theUser = new AppUser()
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
                        return Ok("User has been added");
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(addedUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }
    }
}