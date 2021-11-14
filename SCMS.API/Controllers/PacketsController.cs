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
    public class PacketsController : ControllerBase
    {
        private readonly IPacketsRepository _packetsRepository;
        private readonly IMapper _mapper;

        public PacketsController(IPacketsRepository packetsRepository, IMapper mapper)
        {
            _packetsRepository = packetsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Packet>> GetPackets() => _packetsRepository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Packet> GetPacket(int id)
        {
            var packet = _packetsRepository.GetSingle(id);

            if (packet == null)
            {
                return NotFound();
            }

            return packet;
        }

        [HttpPost]
        public IActionResult PostPacket([FromBody] CreatePacketDto createPacketDto)
        {
            if (createPacketDto == null)
            {
                return BadRequest();
            }

            var packet = _mapper.Map<CreatePacketDto, Packet>(createPacketDto);
            _packetsRepository.Add(packet);

            if (!_packetsRepository.Save())
            {
                throw new Exception("Creating a activity failed on save.");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutPacket(int id, Packet packet)
        {
            if (packet == null)
            {
                return BadRequest();
            }

            var existingPacket = _packetsRepository.GetSingle(id);

            if (existingPacket == null)
            {
                return NotFound();
            }

            _mapper.Map(packet, existingPacket);

            _packetsRepository.Update(existingPacket);

            if (!_packetsRepository.Save())
            {
                throw new Exception("Updating a user failed on save.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePacket(int id)
        {
            var packet = _packetsRepository.GetSingle(id);

            if (packet == null)
            {
                return NotFound();
            }

            _packetsRepository.Delete(id);

            if (!_packetsRepository.Save())
            {
                throw new Exception("Deleting a user failed on save.");
            }

            return Ok();
        }
    }
}