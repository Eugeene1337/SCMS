using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using SCMS.API;
using SCMS.API.Data;
using SCMS.API.Models;
using SCMS.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SCMS.Tests.IntegrationTests
{
    [TestFixture]
    class ControllersTests
    {
        [Test]
        public async Task GetActivities_SendRequest_ShouldReturnActivities()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builer =>
            {
                builer.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("scms_db");
                    });

                    var orderService = services.SingleOrDefault(d => d.ServiceType == typeof(IActivitiesRepository));

                    services.Remove(orderService);

                    var mockService = new Mock<IActivitiesRepository>();

                    mockService.Setup(x => x.GetAll()).Returns(new List<Activity>());

                    services.AddTransient(x => mockService.Object);
                });
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act

            HttpResponseMessage response = await httpClient.GetAsync("api/Activities");

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetActivity_SendRequest_ShouldReturnNotFound()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("scms_db");
                    });

                    var orderService = services.SingleOrDefault(d => d.ServiceType == typeof(IActivitiesRepository));

                    services.Remove(orderService);


                    var mockService = new Mock<IActivitiesRepository>();

                    services.AddTransient(x => mockService.Object);
                });
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act

            HttpResponseMessage response = await httpClient.GetAsync("api/Activity/123");

            // Assert

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task GetUsers_SendRequest_ShouldReturnUsers()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builer =>
            {
                builer.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("scms_db");
                    });

                    var orderService = services.SingleOrDefault(d => d.ServiceType == typeof(IUsersRepository));

                    services.Remove(orderService);

                    var mockService = new Mock<IUsersRepository>();

                    mockService.Setup(x => x.GetAll()).Returns(new List<User>());

                    services.AddTransient(x => mockService.Object);
                });
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act

            HttpResponseMessage response = await httpClient.GetAsync("api/Users");

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetUsers_SendRequest_ShouldReturnNotFound()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("scms_db");
                    });

                    var orderService = services.SingleOrDefault(d => d.ServiceType == typeof(IUsersRepository));

                    services.Remove(orderService);


                    var mockService = new Mock<IUsersRepository>();

                    services.AddTransient(x => mockService.Object);
                });
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act

            HttpResponseMessage response = await httpClient.GetAsync("api/User/17BF2B21-D541-4A8B-FA6F-08D9BF304E74");

            // Assert

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task GetAnnouncements_SendRequest_ShouldReturnAnnouncements()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builer =>
            {
                builer.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("scms_db");
                    });

                    var orderService = services.SingleOrDefault(d => d.ServiceType == typeof(IAnnouncementsRepository));

                    services.Remove(orderService);

                    var mockService = new Mock<IAnnouncementsRepository>();

                    mockService.Setup(x => x.GetAll()).Returns(new List<Announcement>());

                    services.AddTransient(x => mockService.Object);

                    var orderService2 = services.SingleOrDefault(d => d.ServiceType == typeof(IUsersRepository));

                    services.Remove(orderService2);

                    var mockService2 = new Mock<IUsersRepository>();


                    services.AddTransient(x => mockService2.Object);
                });
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act

            HttpResponseMessage response = await httpClient.GetAsync("api/Announcements");

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
