using AutoMapper;
using Moq;
using NUnit.Framework;
using SCMS.API.Controllers;
using SCMS.API.DTO;
using SCMS.API.MappingProfiles;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;

namespace SCMS.Tests.Controllers
{
    [TestFixture]
    public class ActivitiesControllerTests
    {
        private ActivitiesController _controller;

        private Mock<IActivitiesRepository> _activitiesRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _activitiesRepositoryMock = new Mock<IActivitiesRepository>();
            
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ActivitiesMappingProfile()));
            _mapper = new Mapper(configuration);

            _controller = new ActivitiesController(_activitiesRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetActivities_Should_ReturnActivities()
        {
            var result = _controller.GetActivities();

            _activitiesRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetActivity_Should_ReturnActivity()
        {
            var activity = new Activity()
            {
                ActivityId = 123,
                ActivityName = "Test",
            };

            _activitiesRepositoryMock.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(activity);

            var result = _controller.GetActivity(123);

            Assert.AreEqual(activity.ActivityId, result.Value.ActivityId);
            Assert.AreEqual(activity.ActivityName, result.Value.ActivityName);
        }

        [Test]
        public void PostActivity_Should_AddActivity()
        {
            var activity = new CreateActivityDto()
            {
                ActivityName = "Test",
            };

            _activitiesRepositoryMock.Setup(x => x.Save()).Returns(true);
            var result = _controller.PostActivity(activity);

            _activitiesRepositoryMock.Verify(x => x.Add(It.IsAny<Activity>()), Times.Once);
        }

        [Test]
        public void PutActivity_Should_UpdateActivity()
        {
            var activity = new Activity()
            {
                ActivityId = 123,
                ActivityName = "Test",
            };

            _activitiesRepositoryMock.Setup(x => x.Save()).Returns(true);
            _activitiesRepositoryMock.Setup(x => x.GetSingle(123)).Returns(activity);

            var result = _controller.PutActivity(123, activity);

            _activitiesRepositoryMock.Verify(x => x.Update(It.IsAny<Activity>()), Times.Once);
        }

        [Test]
        public void DeleteActivity_Should_DeleteActivity()
        {
            var activity = new Activity()
            {
                ActivityId = 123,
                ActivityName = "Test",
            };

            _activitiesRepositoryMock.Setup(x => x.Save()).Returns(true);
            _activitiesRepositoryMock.Setup(x => x.GetSingle(123)).Returns(activity);

            var result = _controller.DeleteActivity(123);

            _activitiesRepositoryMock.Verify(x => x.Delete(123), Times.Once);
        }
    }
}
