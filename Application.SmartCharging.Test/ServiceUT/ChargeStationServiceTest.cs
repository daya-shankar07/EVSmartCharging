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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.SmartCharging.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ChargeStationServiceTest
    {
        private Mock<IConfiguration> configuration;
        private Mock<ITelemetryAdaptor> telemetryAdaptor;
        private Mock<IMapper> mapper;
        private ChargeStationService cStationService;
        private Mock<IChargeStationRepository> cStationRepository;
        public Guid sId = Guid.Parse("a40a5c72-d741-418d-aba3-7c7addd31bb3");
        public Guid gId = Guid.Parse("98f84d22 - 6236 - 4b65 - b90b - a8c0c3df01b1");

        private CStationRequest cStationRequestPost;
        private CStationRequest cStationRequestUpdate;
        private Cstation cStationResponsePost;
        private Cstation cStationResponseUpdate;


        [TestInitialize]
        public void BeforeEach()
        {
            #region Data Prep
            IEnumerable<Cstation> cStationList = new List<Cstation>() { new Cstation() { StationId = sId, GroupId = gId, Name = "CStation1" } };
            Cstation cstation = new() { StationId = sId, GroupId = gId, Name = "CStation3" };

            IEnumerable<Connector> connectorList = new List<Connector> { new Connector() { Id = 2, MaxCurrent = 200, CstationId = sId, Cstation = new Cstation() { } } };

            Connector connector = new() { Id = 2, MaxCurrent = 300, CstationId = sId };

            cStationRequestPost = new CStationRequest() { Name = "CStation1", Connectors = new List<ConnectorResponse>() { new ConnectorResponse() { Id = 1, CStationId = sId.ToString(), MaxCurrent = 200 } } };
            cStationRequestUpdate = new CStationRequest() { Name = "CStation2", Connectors = new List<ConnectorResponse>() { new ConnectorResponse() { Id = 2, CStationId = sId.ToString(), MaxCurrent = 300 } } };

            cStationResponsePost = new Cstation() { Name = "CStation1", StationId = sId };
            cStationResponseUpdate = new Cstation() { Name = "CStation2", StationId = sId };

            #endregion

            // mock object
            configuration = new Mock<IConfiguration>();
            telemetryAdaptor = new Mock<ITelemetryAdaptor>();
            mapper = new Mock<IMapper>();
            cStationRepository = new Mock<IChargeStationRepository>();
            cStationService = new ChargeStationService(configuration.Object, telemetryAdaptor.Object, cStationRepository.Object, mapper.Object);


            // mock calls to DB
            cStationRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(cStationList));
            cStationRepository.Setup(x => x.GetStationAsync(It.IsAny<string>())).Returns(Task.FromResult(cstation));
            cStationRepository.Setup(x => x.PostAsync(It.IsAny<Cstation>(), It.IsAny<string>())).Returns(Task.FromResult(cStationResponsePost));
            cStationRepository.Setup(x => x.UpdateAsync(It.IsAny<Cstation>(), It.IsAny<string>())).Returns(Task.FromResult(cStationResponseUpdate));
        }


        [TestMethod]
        public void GetAllAsyncTest()
        {
            // Act
            var response = cStationService.GetAllAsync().Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetGroupAsyncTest()
        {

            // Act
            var response = cStationService.GetStationAsync(sId.ToString()).Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void PostAsyncTest()
        {
            // Act
            var response = cStationService.PostAsync(cStationRequestPost, "98f84d22-6236-4b65-b90b-a8c0c3df01b1").Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Connectors.Count > 0);
            Assert.IsTrue(response.Name == cStationRequestUpdate.Name);
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            // Act
            var response = cStationService.UpdateAsync(cStationRequestUpdate, "98f84d22-6236-4b65-b90b-a8c0c3df01b1").Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Name == cStationRequestUpdate.Name);
            Assert.IsTrue(response.GroupId == "98f84d22-6236-4b65-b90b-a8c0c3df01b1");
        }
    }
}
