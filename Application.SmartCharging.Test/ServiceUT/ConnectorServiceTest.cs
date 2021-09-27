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
        private Mock<IMapper> mapper;
        private ConnectorService connectorService;
        private Mock<IConnectorRepository> connectorRepository;
        public Guid gIdGet = Guid.NewGuid();


        #region Data Preparation


        List<Cstation> cstation = new List<Cstation>() { new Cstation() { StationId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Name = "CStation1" } };

        IEnumerable<Connector> connectorList = new List<Connector> { new Connector() { Id = 2, MaxCurrent = 200, CstationId = Guid.NewGuid(), Cstation = new Cstation() { } } };

        Connector connector = new Connector() { Id = 2, MaxCurrent = 300, CstationId = Guid.NewGuid() };

        ConnectorRequest connRequestPost = new ConnectorRequest() { Id=2, MaxCurrent=200 };
        ConnectorRequest connRequestUpdate = new ConnectorRequest() { MaxCurrent = 600, Id=2};

        Connector connResponsePost = new Connector() { Id = 2, MaxCurrent = 200, CstationId=Guid.NewGuid() };
        Connector connectorResponseUpdate = new Connector() { Id=2, MaxCurrent=600, CstationId = Guid.NewGuid() };

        #endregion

        [TestInitialize]
        public void BeforeEach()
        {
            configuration = new Mock<IConfiguration>();
            telemetryAdaptor = new Mock<ITelemetryAdaptor>();
            mapper = new Mock<IMapper>();
            connectorRepository = new Mock<IConnectorRepository>();
            connectorService = new ConnectorService(configuration.Object, telemetryAdaptor.Object, connectorRepository.Object, mapper.Object);


            // mock calls to DB
            connectorRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(connectorList));
            connectorRepository.Setup(x => x.GetConnectorAsync(It.IsAny<string>(),It.IsAny<string>())).Returns(Task.FromResult(connector));
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
            var response = connectorService.GetConnectorAsync(gIdGet.ToString(), "1234").Result;
            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void PostAsyncTest()
        {
            // Act
            var response = connectorService.PostConnectorAsync(connRequestPost,"1234").Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.MaxCurrent == connRequestPost.MaxCurrent);
            Assert.IsTrue(response.Id == connRequestPost.Id);
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            // Act
            var response = connectorService.UpdateConnectorAsync(connRequestUpdate, "1234").Result;
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.MaxCurrent == connRequestUpdate.MaxCurrent);
            Assert.IsTrue(response.Id == connRequestUpdate.Id);
        }
    }
}
