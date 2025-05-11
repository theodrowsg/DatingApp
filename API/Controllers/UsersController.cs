using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
        [AllowAnonymous]
        [HttpGet]
        public  async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        { 
            var users = await userRepository.GetAllMembersAsync();
            return Ok(users);
        }
    
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await userRepository.GetMemberDtoByIdAsync(id);
    
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [Authorize]
        [HttpGet("username/{username}")]
        public async Task<ActionResult<AppUser>> GetUser(string username)
        {
            var user = await userRepository.GetMemberDtoByUserName(username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

}