using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;
using Orders.Shared.DTOs;

namespace OralData.UnitTest.Controllers
{
    [TestClass]
    public class SpecialtieControllerTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Specialtie>> _UnitOfWorkmock = null!;

        public SpecialtieControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _UnitOfWorkmock = new Mock<IGenericUnitOfWork<Specialtie>>();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new SpecialtieController(_UnitOfWorkmock.Object, context);
            var pagination = new PaginationDTO { Filter = "Some" };

            // Act
            var result = await controller.GetAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result .StatusCode);

            // Clean Up
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]

        public async Task GetPagesAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new SpecialtieController(_UnitOfWorkmock.Object, context);
            var pagination = new PaginationDTO { Filter = "Some" };

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean Up
            context.Database.EnsureDeleted();
            context.Dispose();


        }
    }
}