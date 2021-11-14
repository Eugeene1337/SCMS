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
    public class ClassesController : ControllerBase
    {
        private readonly IClassesRepository _classesRepository;
        private readonly IMapper _mapper;

        public ClassesController(IClassesRepository classesRepository, IMapper mapper)
        {
            _classesRepository = classesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Class>> GetClasses() => _classesRepository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Class> GetClass(int id)
        {
            var classs = _classesRepository.GetSingle(id);

            if (classs == null)
            {
                return NotFound();
            }

            return classs;
        }

        [HttpPost]
        public IActionResult PostClass([FromBody] CreateClassDto createClassDto)
        {
            if (createClassDto == null)
            {
                return BadRequest();
            }

            var classs = _mapper.Map<CreateClassDto, Class>(createClassDto);
            _classesRepository.Add(classs);

            if (!_classesRepository.Save())
            {
                throw new Exception("Creating a activity failed on save.");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutClass(int id, Class classs)
        {
            if (classs == null)
            {
                return BadRequest();
            }

            var existingActivity = _classesRepository.GetSingle(id);

            if (existingActivity == null)
            {
                return NotFound();
            }

            _mapper.Map(classs, existingActivity);

            _classesRepository.Update(existingActivity);

            if (!_classesRepository.Save())
            {
                throw new Exception("Updating a user failed on save.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClass(int id)
        {
            var classs = _classesRepository.GetSingle(id);

            if (classs == null)
            {
                return NotFound();
            }

            _classesRepository.Delete(id);

            if (!_classesRepository.Save())
            {
                throw new Exception("Deleting a user failed on save.");
            }

            return Ok();
        }
    }
}
