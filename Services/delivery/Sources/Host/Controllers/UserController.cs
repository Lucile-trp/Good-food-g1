using Host.Interfaces.Services;
using Host.Dto;
using Host.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asp.Versioning;
using System.Collections.Generic;

namespace Host.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET 
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public IActionResult GetUsersV1()
        {
            var useres = _userService.GetUsers();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(useres);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userDtos);
        }

        [HttpGet("{userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByIdV1(int userId)
        {
            if (!_userService.UserExists(userId))
                return NotFound("User not found.");

            var user = _userService.GetUserById(userId);
            var userDto = _mapper.Map<UserDto>(user);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userDto);
        }

        // CREATE
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserV1([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEntity = _mapper.Map<User>(userCreate);

            var success = _userService.CreateUser(userEntity);
            if (!success)
                return BadRequest("Error creating the user.");

            return StatusCode(201, "Successfully created");
        }

        // UPDATE 
        [HttpPut("{userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserV1(int userId, [FromBody] UserDto userUpdate)
        {
            if (userUpdate == null || userId <= 0)
                return BadRequest(ModelState);

            if (!_userService.UserExists(userId))
                return NotFound("User not found.");

            var userEntity = _mapper.Map<User>(userUpdate);
            userEntity.UserId = userId;

            var success = _userService.UpdateUser(userEntity);
            if (!success)
                return BadRequest("Error updating the user.");

            return NoContent();
        }

        // DELETE 
        [HttpDelete("{userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserV1(int userId)
        {
            if (!_userService.UserExists(userId))
                return NotFound("User not found.");

            var success = _userService.DeleteUser(userId);
            if (!success)
                return BadRequest("Error deleting the user.");

            return NoContent();
        }
    }
}
