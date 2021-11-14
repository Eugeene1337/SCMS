using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivitiesRepository activitiesRepository, IMapper mapper)
        {
            _activitiesRepository = activitiesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Activity>> GetActivities() => _activitiesRepository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Activity> GetActivity(int id)
        {
            var activity = _activitiesRepository.GetSingle(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        [HttpPost]
        public IActionResult PostActivity([FromBody] Activity activity)
        {
            if (activity == null)
            {
                return BadRequest();
            }

            _activitiesRepository.Add(activity);

            if (!_activitiesRepository.Save())
            {
                throw new Exception("Creating a activity failed on save.");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutActivity(int id, Activity activity)
        {
            if (activity == null)
            {
                return BadRequest();
            }

            var existingActivity = _activitiesRepository.GetSingle(id);

            if (existingActivity == null)
            {
                return NotFound();
            }

            _mapper.Map(activity, existingActivity);

            _activitiesRepository.Update(existingActivity);

            if (!_activitiesRepository.Save())
            {
                throw new Exception("Updating a user failed on save.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActivity(int id)
        {
            var activity = _activitiesRepository.GetSingle(id);

            if (activity == null)
            {
                return NotFound();
            }

            _activitiesRepository.Delete(id);

            if (!_activitiesRepository.Save())
            {
                throw new Exception("Deleting a user failed on save.");
            }

            return Ok();
        }
    }
}
