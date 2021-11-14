using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SCMS.API.DTO;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetUserDto>> GetUsers()
        {
            return _mapper.Map<List<User>, List<GetUserDto>>(_userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<GetUserDto> GetUser(Guid id)
        {
            var user = _mapper.Map<User, GetUserDto>(_userRepository.GetSingle(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto userUpdateDto)
        {
            if (userUpdateDto == null)
            {
                return BadRequest();
            }

            var existingUser = _userRepository.GetSingle(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _mapper.Map(userUpdateDto, existingUser);

            _userRepository.Update(existingUser);

            if (!_userRepository.Save())
            {
                throw new Exception("Updating a user failed on save.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(Guid id)
        {
            var user = _userRepository.GetSingle(id);

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(id);

            if (!_userRepository.Save())
            {
                throw new Exception("Deleting a user failed on save.");
            }

            return Ok();
        }
    }
}
