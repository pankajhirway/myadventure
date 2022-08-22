using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using MyAdventureAPI.Controllers;
using MyAdventureAPI.models;
using MyAdventureAPI.Service;
using Xunit;

namespace MyAdventureAPI.Tests
{
    public class AdventureControllerTest
    {
        public AdventureControllerTest()
        {
        }

        [Fact]
        public async void AdventureControllerTest_Get()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AdventureController>>();
            var mockAdventureService = new Mock<AdventureService>();

            var data = new List<Adventure>();

            mockAdventureService.Setup(_ => _.GetAsync()).Returns(Task.FromResult(data));
            var controller = new AdventureController(mockAdventureService.Object,mockLogger.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.Empty(result);
            
        }
    }
}
