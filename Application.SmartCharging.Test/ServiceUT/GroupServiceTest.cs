using Application.SmartCharging.BL;
using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using Application.SmartCharging.EFCore.Models;
using Application.SmartCharging.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.SmartCharging.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GroupServiceTest
    {
        private Mock<IConfiguration> configuration;
        private Mock<ITelemetryAdaptor> telemetryAdaptor;
        private Mock<IMapper> mapper;
        private GroupService groupService;
        private Mock<IGroupRepository> groupRepository;
       public Guid gIdGet = Guid.NewGuid();


        #region Data Preparation


        List<Cstation> cstation = new List<Cstation>() { new Cstation() {StationId=Guid.NewGuid(), GroupId = Guid.NewGuid(), Name="CStation1" } };
      
        IEnumerable<Group> groupList = new List<Group> { new Group() { Id = Guid.NewGuid(), Capacity = 1000, Name = "Test1", Cstations = new List<Cstation>() { new Cstation() { StationId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Name = "CStation1" } } } };

        Group group = new() { Id = Guid.NewGuid(), Capacity = 1000, Name = "Test2", Cstations = new List<Cstation>() { new Cstation() { StationId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Name = "CStation1" } } };

        GroupRequest gpRPost = new GroupRequest() { Capacity = 1000, Name = "Group2" };
        GroupRequest gpRUpdate = new GroupRequest() { Capacity = 9000, Name = "Group3" };

        Group gpPost = new Group() { Id = Guid.NewGuid(), Capacity = 1000, Name = "Group2" };
        Group gpUpdate = new Group() { Id = Guid.NewGuid(), Capacity = 9000, Name = "Group3" };

        #endregion

        [TestInitialize]
        public void BeforeEach()
        {
            configuration = new Mock<IConfiguration>();
            telemetryAdaptor = new Mock<ITelemetryAdaptor>();
            mapper = new Mock<IMapper>();
            groupRepository = new Mock<IGroupRepository>(); 
            groupService = new GroupService(configuration.Object,telemetryAdaptor.Object,mapper.Object,groupRepository.Object);


            // mock calls to DB
            groupRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(groupList));
            groupRepository.Setup(x => x.GetGroupAsync(It.IsAny<string>())).Returns(Task.FromResult(group));
            groupRepository.Setup(x => x.PostAsync(gpPost)).Returns(Task.FromResult(gpPost));
            groupRepository.Setup(x => x.UpdateAsync(gpUpdate)).Returns(Task.FromResult(gpUpdate));
        }


        [TestMethod]
        public void GetAllAsyncTest()
        {
            // Act
            var response = groupService.GetAllGroupAsync().Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetGroupAsyncTest()
        {

            // Act
            var response = groupService.GetGroupAsync(gIdGet.ToString()).Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void PostAsyncTest()
        {
            // Act
            var response = groupService.PostGroupAsync(gpRPost).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Name==gpPost.Name);
            Assert.IsTrue(response.Capacity == gpPost.Capacity);
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            // Act
            var response = groupService.UpdateGroupAsync(gpRUpdate).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Name == gpRUpdate.Name);
            Assert.IsTrue(response.Capacity == gpRUpdate.Capacity);
        }
    }
}
