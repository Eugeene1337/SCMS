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
    public class AnnouncementsControllerTests
    {
        private AnnouncementsController _controller;

        private Mock<IAnnouncementsRepository> _announcementsRepositoryMock;
        private Mock<IUsersRepository> _usersRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _announcementsRepositoryMock = new Mock<IAnnouncementsRepository>();
            _usersRepositoryMock = new Mock<IUsersRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new AnnouncementsMappingProfile()));
            _mapper = new Mapper(configuration);

            _controller = new AnnouncementsController(_announcementsRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetAnnouncements_Should_ReturnAnnouncements()
        {
            List<Announcement> announcements = new List<Announcement>();
            _announcementsRepositoryMock.Setup(x => x.GetAll()).Returns(announcements);

            var result = _controller.GetAnnouncements();

            _announcementsRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetAnnouncement_Should_ReturnAnnouncement()
        {
            var announcement = new Announcement()
            {
                AnnouncementId = 123,
                Text = "Test",
                CreatedBy = Guid.NewGuid().ToString(),
            };

            var user = new User()
            {
                Name = "Name",
                Surname = "Surname"
            };

            _announcementsRepositoryMock.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(announcement);
            _usersRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns(user);

            var result = _controller.GetAnnouncement(123);

            Assert.AreEqual(announcement.AnnouncementId, result.Value.AnnouncementId);
            Assert.AreEqual(announcement.Text, result.Value.Text);
        }

        [Test]
        public void PostAnnouncement_Should_AddAnnouncement()
        {
            var announcement = new CreateAnnouncementDto()
            {
                Text = "Test",
                CreatedBy = Guid.NewGuid().ToString(),
            };

            var user = new User()
            {
                Name = "Name",
                Surname = "Surname"
            };

            _announcementsRepositoryMock.Setup(x => x.Save()).Returns(true);
            _usersRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns(user);

            var result = _controller.PostAnnouncement(announcement);

            _announcementsRepositoryMock.Verify(x => x.Add(It.IsAny<Announcement>()), Times.Once);
        }

        [Test]
        public void PutAnnouncement_Should_UpdateAnnouncement()
        {
            var announcement = new UpdateAnnouncementDto()
            {
                Text = "Test"
            };

            _announcementsRepositoryMock.Setup(x => x.Save()).Returns(true);
            _announcementsRepositoryMock.Setup(x => x.GetSingle(123)).Returns(new Announcement());

            var result = _controller.PutAnnouncement(123, announcement);

            _announcementsRepositoryMock.Verify(x => x.Update(It.IsAny<Announcement>()), Times.Once);
        }

        [Test]
        public void DeleteAnnouncement_Should_DeleteAnnouncement()
        {
            var announcement = new Announcement()
            {
                AnnouncementId = 123,
                Text = "Test"
            };

            _announcementsRepositoryMock.Setup(x => x.Save()).Returns(true);
            _announcementsRepositoryMock.Setup(x => x.GetSingle(123)).Returns(announcement);

            var result = _controller.DeleteAnnouncement(123);

            _announcementsRepositoryMock.Verify(x => x.Delete(123), Times.Once);
        }
    }
}
