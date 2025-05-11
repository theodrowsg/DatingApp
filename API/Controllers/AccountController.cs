using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok("Get all accounts");
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            return Ok($"Get account with ID: {id}");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");

           return Ok();
        //    using var hmac = new HMACSHA512();
        //     var user = new AppUser
        //     {
        //         UserName = registerDto.Username.ToLower(),
        //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //         PasswordSalt = hmac.Key
        //     };

        //     context.Users.Add(user);
        //     await context.SaveChangesAsync();
        //     return new UserDto  {
        //         Username = user.UserName,
        //         Token = tokenService.CreateToken(user)};
               
        }


        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] object account)
        {
            return Ok($"Account with ID: {id} updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            return Ok($"Account with ID: {id} deleted");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }   
    }
}