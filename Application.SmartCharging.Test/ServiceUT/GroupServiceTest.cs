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
        private IMapper mapper;
        private GroupService groupService;
        private Mock<IGroupRepository> groupRepository;
        public Guid sId = Guid.Parse("a40a5c72-d741-418d-aba3-7c7addd31bb3");
        public Guid gId = Guid.Parse("98f84d22-6236-4b65-b90b-a8c0c3df01b1");
        private GroupRequest gpRequestPost;
        private GroupRequest gpRequestUpdate;
        private Group gpReponsePost;
        private Group gpResponseUpdate;



        [TestInitialize]
        public void BeforeEach()
        {
            #region Data Preparation
            List<Cstation> cstationList = new List<Cstation>() { new Cstation() { StationId = sId, GroupId = gId, Name = "CStation1" } };
            List<CStationRequest> cstationReqList = new List<CStationRequest>() { new CStationRequest() {  Name="Station1",Connectors=null } };

            IEnumerable<Group> groupList = new List<Group> { new Group() { Id = gId, Capacity = 1000, Name = "Test1", Cstations = new List<Cstation>() { new Cstation() { StationId = sId, Name = "CStation1" } } } };
            Group group = new() { Id = gId, Capacity = 1000, Name = "Test2", Cstations = new List<Cstation>() { new Cstation() { StationId = sId, Name = "CStation1" } } };
            gpRequestPost = new GroupRequest() { Capacity = 1000, Name = "Group2",CStations= cstationReqList };
            gpRequestUpdate = new GroupRequest() { Capacity = 9000, Name = "Group3" };
            gpReponsePost = new Group() { Id = gId, Capacity = 1000, Name = "Group2" ,Cstations= cstationList };
            gpResponseUpdate = new Group() { Id = gId, Capacity = 9000, Name = "Group3" };
            #endregion

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });
            mapper = mapperConfig.CreateMapper();
            configuration = new Mock<IConfiguration>();
            telemetryAdaptor = new Mock<ITelemetryAdaptor>();
            groupRepository = new Mock<IGroupRepository>();
            groupService = new GroupService(configuration.Object, telemetryAdaptor.Object, mapper, groupRepository.Object);


            // mock calls to DB
            groupRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(groupList));
            groupRepository.Setup(x => x.GetGroupAsync(It.IsAny<string>())).Returns(Task.FromResult(group));
            groupRepository.Setup(x => x.PostAsync(It.IsAny<Group>())).Returns(Task.FromResult(gpReponsePost));
            groupRepository.Setup(x => x.UpdateAsync(It.IsAny<Group>())).Returns(Task.FromResult(gpResponseUpdate));
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
            var response = groupService.GetGroupAsync(gId.ToString()).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Capacity == 1000); // 
        }

        [TestMethod]
        public void PostAsyncTest()
        {
            // Act
            var response = groupService.PostGroupAsync(gpRequestPost).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Name == gpRequestPost.Name);
            Assert.IsTrue(response.Capacity == gpRequestPost.Capacity);
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            // Act
            var response = groupService.UpdateGroupAsync(gpRequestUpdate).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Name == gpRequestUpdate.Name);
            Assert.IsTrue(response.Capacity == gpRequestUpdate.Capacity);
        }
    }
}
