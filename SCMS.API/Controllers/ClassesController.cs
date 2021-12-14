using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SCMS.API.Converters.Interfaces;
using SCMS.API.DTO;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassesRepository _classesRepository;
        private readonly IClassesConverter _classesConverter;
        private readonly IMapper _mapper;

        public ClassesController(IClassesRepository classesRepository, IClassesConverter classesConverter, IMapper mapper)
        {
            _classesRepository = classesRepository;
            _classesConverter = classesConverter;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public ActionResult<IEnumerable<GetClassDto>> GetClassesWeek(string userId)
        {
            var classes = _classesRepository.GetClassesWeek(userId);
            List<GetClassDto> classesDtos = _classesConverter.Convert(classes);

            return classesDtos;
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public ActionResult<IEnumerable<GetClassDto>> GetClasses(string userId)
        {
            var classes = _classesRepository.GetClasses(userId);
            List<GetClassDto> classesDtos = _classesConverter.Convert(classes);

            return classesDtos;
        }

        [HttpGet]
        [Route("[action]/{classId}")]
        public ActionResult<IEnumerable<GetUserDto>> GetEnrolledUsers(int classId)
        {
            return _mapper.Map<List<User>, List<GetUserDto>>(_classesRepository.GetEnrolledUsers(classId));
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetClassDto>> GetClasses()
        {
            var classes = _classesRepository.GetAll();
            List<GetClassDto> classesDtos = _classesConverter.Convert(classes);

            return classesDtos;
        } 

        [HttpGet("{id}")]
        public ActionResult<GetClassDto> GetClass(int id)
        {
            var classs = _classesRepository.GetSingle(id);

            if (classs == null)
            {
                return NotFound();
            }

            var getClassDto = _mapper.Map<GetClassDto>(classs);

            return getClassDto;
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

            var classes = _classesRepository.GetAll();

            var clas = classes.OrderByDescending(x => x.ClassId).FirstOrDefault();
            CreateClassReturnModel returnModel = new CreateClassReturnModel()
            {
                ClassId = clas.ClassId,
            };

            return Ok(returnModel);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Enroll([FromBody] ClassEnrollment classEnrollment)
        {
            if (classEnrollment == null)
            {
                return BadRequest();
            }

            _classesRepository.Add(classEnrollment);

            if (!_classesRepository.Save())
            {
                throw new Exception("Creating a activity failed on save.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult DeleteEnrollment([FromBody] ClassEnrollment classEnrollment)
        {
            if (classEnrollment == null)
            {
                return BadRequest();
            }

            _classesRepository.Delete(classEnrollment);

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
