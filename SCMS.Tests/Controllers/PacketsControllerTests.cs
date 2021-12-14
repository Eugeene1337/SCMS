using AutoMapper;
using Moq;
using NUnit.Framework;
using SCMS.API.Controllers;
using SCMS.API.DTO;
using SCMS.API.MappingProfiles;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCMS.Tests.Controllers
{
    [TestFixture]
    class PacketsControllerTests
    {
        private PacketsController _controller;

        private Mock<IPacketsRepository> _packetsRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _packetsRepositoryMock = new Mock<IPacketsRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new PacketsMappingProfile()));
            _mapper = new Mapper(configuration);

            _controller = new PacketsController(_packetsRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetPackets_Should_ReturnPackets()
        {
            List<Packet> packets = new List<Packet>();
            _packetsRepositoryMock.Setup(x => x.GetAll()).Returns(packets);

            var result = _controller.GetPackets();

            _packetsRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetPacket_Should_ReturnPacket()
        {
            var packet = new Packet()
            {
                PacketId = 123,
                PacketName = "Test",
                PacketPrice = 123.0
            };

            _packetsRepositoryMock.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(packet);

            var result = _controller.GetPacket(123);

            Assert.AreEqual(packet.PacketId, result.Value.PacketId);
            Assert.AreEqual(packet.PacketName, result.Value.PacketName);
            Assert.AreEqual(packet.PacketPrice, result.Value.PacketPrice);
        }

        [Test]
        public void PostPacket_Should_AddPacket()
        {
            var activity = new CreatePacketDto()
            {
                PacketName = "Test",
                PacketPrice = 123.0
            };

            _packetsRepositoryMock.Setup(x => x.Save()).Returns(true);
            var result = _controller.PostPacket(activity);

            _packetsRepositoryMock.Verify(x => x.Add(It.IsAny<Packet>()), Times.Once);
        }

        [Test]
        public void PutPacket_Should_UpdateActivity()
        {
            var packet = new Packet()
            {
                PacketId = 123,
                PacketName = "Test",
                PacketPrice = 123.0
            };


            _packetsRepositoryMock.Setup(x => x.Save()).Returns(true);
            _packetsRepositoryMock.Setup(x => x.GetSingle(123)).Returns(packet);

            var result = _controller.PutPacket(123, packet);

            _packetsRepositoryMock.Verify(x => x.Update(It.IsAny<Packet>()), Times.Once);
        }

        [Test]
        public void DeleteActivity_Should_DeleteActivity()
        {
            var packet = new Packet()
            {
                PacketId = 123,
                PacketName = "Test",
                PacketPrice = 123.0
            };


            _packetsRepositoryMock.Setup(x => x.Save()).Returns(true);
            _packetsRepositoryMock.Setup(x => x.GetSingle(123)).Returns(packet);

            var result = _controller.DeletePacket(123);

            _packetsRepositoryMock.Verify(x => x.Delete(123), Times.Once);
        }
    }
}
