using System;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTests
    {
        public AnalyticsControllerTests()
        {
            _analyticsController = new AnalyticsController(_analyticsRepositoryMock.Object, _panelRepositoryMock.Object);
        }

        private readonly AnalyticsController _analyticsController;

        private readonly Mock<IAnalyticsRepository> _analyticsRepositoryMock = new Mock<IAnalyticsRepository>();
        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();

        [Fact]
        public async Task Post_ShouldInsertPanel()
        {
            var oneHour = new OneHourElectricityModel
            {
                KiloWatt = 1,
            };

            // Arrange

            // Act
            var result = await _analyticsController.Post("panelID", oneHour);

            // Assert
            Assert.NotNull(result);

            _analyticsRepositoryMock.Verify(x => 
            x.InsertAsync(It.IsAny<OneHourElectricity>()),Times.Once());

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

            _analyticsRepositoryMock.ResetCalls();
        }
    }
}