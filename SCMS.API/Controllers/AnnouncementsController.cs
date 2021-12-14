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
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementsRepository _announcementsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public AnnouncementsController(IAnnouncementsRepository announcementsRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _announcementsRepository = announcementsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Announcement>> GetAnnouncements()
        {
            var announcements = _announcementsRepository.GetAll();

            foreach(var item in announcements)
            {
                var user = _usersRepository.GetSingle(Guid.Parse(item.CreatedBy));

                item.CreatedBy = $"{user.Name} {user.Surname}";
            }

            return announcements;
        }

        [HttpGet("{id}")]
        public ActionResult<Announcement> GetAnnouncement(int id)
        {
            var announcement = _announcementsRepository.GetSingle(id);
            var user = _usersRepository.GetSingle(Guid.Parse(announcement.CreatedBy));

            announcement.CreatedBy = $"{user.Name} {user.Surname}"; 

            if (announcement == null)
            {
                return NotFound();
            }

            return announcement;
        }

        [HttpPost]
        public IActionResult PostAnnouncement([FromBody] CreateAnnouncementDto createAnnouncementDto)
        {
            if (createAnnouncementDto == null)
            {
                return BadRequest();
            }

            var user = _usersRepository.GetSingle(Guid.Parse(createAnnouncementDto.CreatedBy));

            if(user == null)
            {
                return NotFound($"User with userId: {createAnnouncementDto.CreatedBy} does not exist");
            }

            var announcement = _mapper.Map<CreateAnnouncementDto, Announcement>(createAnnouncementDto);

            _announcementsRepository.Add(announcement);

            if (!_announcementsRepository.Save())
            {
                throw new Exception("Creating a activity failed on save.");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutAnnouncement(int id, UpdateAnnouncementDto updateAnnouncementDto)
        {
            if (updateAnnouncementDto == null)
            {
                return BadRequest();
            }

            var existingActivity = _announcementsRepository.GetSingle(id);

            if (existingActivity == null)
            {
                return NotFound();
            }

            _mapper.Map(updateAnnouncementDto, existingActivity);

            _announcementsRepository.Update(existingActivity);

            if (!_announcementsRepository.Save())
            {
                throw new Exception("Updating a user failed on save.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnnouncement(int id)
        {
            var announcement = _announcementsRepository.GetSingle(id);

            if (announcement == null)
            {
                return NotFound();
            }

            _announcementsRepository.Delete(id);

            if (!_announcementsRepository.Save())
            {
                throw new Exception("Deleting a user failed on save.");
            }

            return Ok();
        }
    }
}
