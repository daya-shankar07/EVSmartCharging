using Application.SmartCharging.BL;
using Application.SmartCharging.Common;
using Application.SmartCharging.DL;
using Application.SmartCharging.EFCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Application.SmartCharging.Test
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class GroupRepositoryTest
    {
        private Mock<ITelemetryAdaptor> telemetryAdaptor;
        private GroupRepository groupRepository;
        private Mock<evsolutionContext> evsolutionContext;
        public Guid gIdGet = Guid.NewGuid();


        [TestInitialize]
        public void BeforeEach()
        {
            telemetryAdaptor = new Mock<ITelemetryAdaptor>();
            evsolutionContext = new Mock<evsolutionContext>();

            // mock
            evsolutionContext.Setup(x => x.Remove(evsolutionContext.Object));
            //groupRepository.Setup(x => x.GetGroupAsync(It.IsAny<string>())).Returns(Task.FromResult(group));
            //groupRepository.Setup(x => x.PostAsync(gpPost)).Returns(Task.FromResult(gpPost));
            //groupRepository.Setup(x => x.UpdateAsync(gpUpdate)).Returns(Task.FromResult(gpUpdate));
        }


            [TestMethod]
        public void DeleteAsyncTest()
        {
            // TODO

        }
    }
}
