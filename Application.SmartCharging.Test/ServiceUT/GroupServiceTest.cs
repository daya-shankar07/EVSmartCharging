using Application.SmartCharging.BL;
using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using Application.SmartCharging.EFCore.Models;
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
       // Guid gId = Guid.NewGuid();


        #region Data Preparation


        List<Cstation> cstation = new List<Cstation>() { new Cstation() {StationId=Guid.NewGuid(), GroupId = Guid.NewGuid(), Name="CStation1" } };
      
        IEnumerable<Group> groupList = new List<Group> { new Group() { Id = Guid.NewGuid(), Capacity = 1000, Name = "Test1", Cstations = new List<Cstation>() { new Cstation() { StationId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Name = "CStation1" } } } };

        //Group gp = new Group() { Id = Guid.NewGuid(), Capacity = 1000, Name = "Test2", Cstations = new Cstation() { StationId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Name = "CStation1" } };

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
           // groupRepository.Setup(x => x.GetGroupAsync(It.IsAny<string>())).Returns(Task.FromResult(groupList));
            groupRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(groupList));
            groupRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(groupList));
            groupRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(groupList));

        }


            [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
