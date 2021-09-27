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
    public class ConnectorServiceTest
    {
        private Mock<IConfiguration> configuration;
        private Mock<ITelemetryAdaptor> telemetryAdaptor;
        private IMapper mapper;
        private ConnectorService connectorService;
        private Mock<IConnectorRepository> connectorRepository;
        public Guid gIdGet = Guid.NewGuid();
        public Guid sId = Guid.Parse("a40a5c72-d741-418d-aba3-7c7addd31bb3");
        public Guid gId = Guid.Parse("98f84d22-6236-4b65-b90b-a8c0c3df01b1");

        private ConnectorRequest connRequestPost;
        private ConnectorRequest connRequestUpdate;
        private Connector connResponsePost;
        private Connector connectorResponseUpdate;



        [TestInitialize]
        public void BeforeEach()
        {
            #region Data Preparation
            List<Cstation> cstationList = new List<Cstation>() { new Cstation() { StationId = sId, GroupId = gId, Name = "CStation1" } };
            IEnumerable<Connector> connectorList = new List<Connector> { new Connector() { Id = 2, MaxCurrent = 200, CstationId = sId, Cstation = new Cstation() { } } };
            Connector connector = new Connector() { Id = 2, MaxCurrent = 300, CstationId = sId };
            connRequestPost = new ConnectorRequest() { Id = 2, MaxCurrent = 200 };
            connRequestUpdate = new ConnectorRequest() { MaxCurrent = 600, Id = 2 };
            connResponsePost = new Connector() { Id = 2, MaxCurrent = 200, CstationId = sId };
            connectorResponseUpdate = new Connector() { Id = 2, MaxCurrent = 600, CstationId = sId };
            #endregion

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainProfile());
            });
            mapper = mapperConfig.CreateMapper();
            configuration = new Mock<IConfiguration>();
            telemetryAdaptor = new Mock<ITelemetryAdaptor>();
            connectorRepository = new Mock<IConnectorRepository>();
            connectorService = new ConnectorService(configuration.Object, telemetryAdaptor.Object, connectorRepository.Object, mapper);

            // mock calls to DB
            connectorRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(connectorList));
            connectorRepository.Setup(x => x.GetConnectorAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(connector));
            connectorRepository.Setup(x => x.PostAsync(It.IsAny<Connector>(), It.IsAny<string>())).Returns(Task.FromResult(connResponsePost));
            connectorRepository.Setup(x => x.UpdateAsync(It.IsAny<Connector>(), It.IsAny<string>())).Returns(Task.FromResult(connectorResponseUpdate));
        }


        [TestMethod]
        public void GetAllAsyncTest()
        {
            // Act
            var response = connectorService.GetAllConnectorAsync().Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetGroupAsyncTest()
        {

            // Act
            var response = connectorService.GetConnectorAsync(gIdGet.ToString(), sId.ToString()).Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void PostAsyncTest()
        {
            // Act
            var response = connectorService.PostConnectorAsync(connRequestPost, sId.ToString()).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.MaxCurrent == connRequestPost.MaxCurrent);
            Assert.IsTrue(response.Id == connRequestPost.Id);
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            // Act
            var response = connectorService.UpdateConnectorAsync(connRequestUpdate, sId.ToString()).Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.MaxCurrent == connRequestUpdate.MaxCurrent);
            Assert.IsTrue(response.Id == connRequestUpdate.Id);

        }
    }
}
