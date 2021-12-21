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

namespace SCMS.Tests.Controllers
{
    [TestFixture]
    class UsersControllerTests
    {
        private UsersController _controller;

        private Mock<IUsersRepository> _usersRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new UsersMappingProfile()));
            _mapper = new Mapper(configuration);

            _controller = new UsersController(_usersRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetUsers_Should_ReturnUsers()
        {
            List<User> users = new List<User>();
            _usersRepositoryMock.Setup(x => x.GetAll()).Returns(users);

            var result = _controller.GetUsers();

            _usersRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetUser_Should_ReturnUser()
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                Surname = "Surname"
            };

            _usersRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns(user);

            var result = _controller.GetUser(Guid.NewGuid());

            Assert.AreEqual(user.Id, result.Value.Id.ToString());
            Assert.AreEqual(user.Name, result.Value.Name);
            Assert.AreEqual(user.Surname, result.Value.Surname);
        }

        [Test]
        public void UpdateUser_Should_UpdateUser()
        {
            var id = Guid.NewGuid();
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                Surname = "Surname"
            };

            var user2 = new UpdateUserDto()
            {
                Name = "Name",
                Surname = "Surname"
            };

            _usersRepositoryMock.Setup(x => x.Save()).Returns(true);
            _usersRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns(user);

            var result = _controller.UpdateUser(id, user2);

            _usersRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void DeleteUser_Should_DeleteUser()
        {
            var id = Guid.NewGuid();

            var user = new User()
            {
                Id = id.ToString(),
                Name = "Name",
                Surname = "Surname"
            };

            _usersRepositoryMock.Setup(x => x.Save()).Returns(true);
            _usersRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns(user);

            var result = _controller.DeleteUser(id);

            _usersRepositoryMock.Verify(x => x.Delete(id), Times.Once);
        }


    }
}
