using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using Moq;
using NUnit.Framework;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FlowCycle.Domain.Storage.Models;
using FlowCycle.Persistance.UnitOfWork;

namespace FlowCycle.Tests.Services
{
    [TestFixture]
    public class CostingServiceTests
    {
        private Mock<ICostingRepository> _costingRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CostingService _costingService;
        private CancellationToken _defaultCancellationToken;

        [SetUp]
        public void Setup()
        {
            _costingRepositoryMock = new Mock<ICostingRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _costingService = new CostingService(_costingRepositoryMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);
            _defaultCancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task GetByIdAsync_WhenCostingExists_ShouldReturnCosting()
        {
            // Arrange
            var expectedCosting = new CostingDao
            {
                Id = 1,
                ProductName = "Test Product",
                ProductCode = "TEST001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var expectedModel = new CostingModel
            {
                Id = 1,
                ProductName = "Test Product",
                ProductCode = "TEST001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1,
                CreatedAt = expectedCosting.CreatedAt,
                UpdatedAt = expectedCosting.UpdatedAt
            };
            _costingRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ReturnsAsync(expectedCosting);
            _mapperMock.Setup(x => x.Map<CostingModel>(expectedCosting))
                .Returns(expectedModel);

            // Act
            var result = await _costingService.GetByIdAsync(1, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ProductName, Is.EqualTo("Test Product"));
            Assert.That(result.ProductCode, Is.EqualTo("TEST001"));
            Assert.That(result.CostingTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("шт"));
            Assert.That(result.Quantity, Is.EqualTo(1));
            Assert.That(result.UnitCost, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(100));
            Assert.That(result.ProjectId, Is.EqualTo(1));
            _costingRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingModel>(expectedCosting), Times.Once);
        }

        [Test]
        public void GetByIdAsync_WhenCostingDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _costingRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _costingService.GetByIdAsync(1, _defaultCancellationToken));
            _costingRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewCosting()
        {
            // Arrange
            var costingModel = new CostingModel
            {
                ProductName = "Test Product",
                ProductCode = "TEST001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1
            };
            var costingDao = new CostingDao
            {
                ProductName = "Test Product",
                ProductCode = "TEST001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var createdDao = new CostingDao
            {
                Id = 1,
                ProductName = "Test Product",
                ProductCode = "TEST001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1,
                CreatedAt = costingDao.CreatedAt,
                UpdatedAt = costingDao.UpdatedAt
            };
            var createdModel = new CostingModel
            {
                Id = 1,
                ProductName = "Test Product",
                ProductCode = "TEST001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1,
                CreatedAt = costingDao.CreatedAt,
                UpdatedAt = costingDao.UpdatedAt
            };

            _mapperMock.Setup(x => x.Map<CostingDao>(costingModel))
                .Returns(costingDao);
            _costingRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CostingDao>(), _defaultCancellationToken))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(x => x.Map<CostingModel>(createdDao))
                .Returns(createdModel);

            // Act
            var result = await _costingService.CreateAsync(costingModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ProductName, Is.EqualTo("Test Product"));
            Assert.That(result.ProductCode, Is.EqualTo("TEST001"));
            Assert.That(result.CostingTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("шт"));
            Assert.That(result.Quantity, Is.EqualTo(1));
            Assert.That(result.UnitCost, Is.EqualTo(100));
            Assert.That(result.TotalCost, Is.EqualTo(100));
            Assert.That(result.ProjectId, Is.EqualTo(1));
            _costingRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingDao>(costingModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingModel>(createdDao), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenCostingExists_ShouldUpdateCosting()
        {
            // Arrange
            var costingModel = new CostingModel
            {
                Id = 1,
                ProductName = "Updated Product",
                ProductCode = "TEST002",
                CostingTypeId = 2,
                Uom = "кг",
                Quantity = 2,
                UnitCost = 200,
                TotalCost = 400,
                ProjectId = 2
            };
            var costingDao = new CostingDao
            {
                Id = 1,
                ProductName = "Updated Product",
                ProductCode = "TEST002",
                CostingTypeId = 2,
                Uom = "кг",
                Quantity = 2,
                UnitCost = 200,
                TotalCost = 400,
                ProjectId = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var updatedDao = new CostingDao
            {
                Id = 1,
                ProductName = "Updated Product",
                ProductCode = "TEST002",
                CostingTypeId = 2,
                Uom = "кг",
                Quantity = 2,
                UnitCost = 200,
                TotalCost = 400,
                ProjectId = 2,
                CreatedAt = costingDao.CreatedAt,
                UpdatedAt = costingDao.UpdatedAt
            };
            var updatedModel = new CostingModel
            {
                Id = 1,
                ProductName = "Updated Product",
                ProductCode = "TEST002",
                CostingTypeId = 2,
                Uom = "кг",
                Quantity = 2,
                UnitCost = 200,
                TotalCost = 400,
                ProjectId = 2,
                CreatedAt = costingDao.CreatedAt,
                UpdatedAt = costingDao.UpdatedAt
            };

            _mapperMock.Setup(x => x.Map<CostingDao>(costingModel))
                .Returns(costingDao);
            _costingRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ReturnsAsync(costingDao);
            _costingRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<CostingDao>(), _defaultCancellationToken))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(x => x.Map<CostingModel>(updatedDao))
                .Returns(updatedModel);

            // Act
            var result = await _costingService.UpdateAsync(costingModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ProductName, Is.EqualTo("Updated Product"));
            Assert.That(result.ProductCode, Is.EqualTo("TEST002"));
            Assert.That(result.CostingTypeId, Is.EqualTo(2));
            Assert.That(result.Uom, Is.EqualTo("кг"));
            Assert.That(result.Quantity, Is.EqualTo(2));
            Assert.That(result.UnitCost, Is.EqualTo(200));
            Assert.That(result.TotalCost, Is.EqualTo(400));
            Assert.That(result.ProjectId, Is.EqualTo(2));
            _costingRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CostingDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingDao>(costingModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingModel>(updatedDao), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_WhenCostingExists_ShouldDeleteCosting()
        {
            // Arrange
            _costingRepositoryMock.Setup(x => x.DeleteAsync(1, _defaultCancellationToken))
                .Returns(Task.CompletedTask);

            // Act
            await _costingService.DeleteAsync(1, _defaultCancellationToken);

            // Assert
            _costingRepositoryMock.Verify(x => x.DeleteAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task GetListAsync_WithFilter_ShouldReturnFilteredCostings()
        {
            // Arrange
            var filter = new CostingFilter { ProductName = "Test" };
            var filterDao = new CostingFilterDao { ProductName = "Test" };
            var expectedDaos = new List<CostingDao>
            {
                new CostingDao
                {
                    Id = 1,
                    ProductName = "Test Product 1",
                    ProductCode = "TEST001",
                    CostingTypeId = 1,
                    Uom = "шт",
                    Quantity = 1,
                    UnitCost = 100,
                    TotalCost = 100,
                    ProjectId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new CostingDao
                {
                    Id = 2,
                    ProductName = "Test Product 2",
                    ProductCode = "TEST002",
                    CostingTypeId = 2,
                    Uom = "кг",
                    Quantity = 2,
                    UnitCost = 200,
                    TotalCost = 400,
                    ProjectId = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            var expectedModels = new List<CostingModel>
            {
                new CostingModel
                {
                    Id = 1,
                    ProductName = "Test Product 1",
                    ProductCode = "TEST001",
                    CostingTypeId = 1,
                    Uom = "шт",
                    Quantity = 1,
                    UnitCost = 100,
                    TotalCost = 100,
                    ProjectId = 1,
                    CreatedAt = expectedDaos[0].CreatedAt,
                    UpdatedAt = expectedDaos[0].UpdatedAt
                },
                new CostingModel
                {
                    Id = 2,
                    ProductName = "Test Product 2",
                    ProductCode = "TEST002",
                    CostingTypeId = 2,
                    Uom = "кг",
                    Quantity = 2,
                    UnitCost = 200,
                    TotalCost = 400,
                    ProjectId = 2,
                    CreatedAt = expectedDaos[1].CreatedAt,
                    UpdatedAt = expectedDaos[1].UpdatedAt
                }
            };

            _mapperMock.Setup(x => x.Map<CostingFilterDao>(filter))
                .Returns(filterDao);
            _costingRepositoryMock.Setup(x => x.GetListAsync(filterDao, _defaultCancellationToken))
                .ReturnsAsync(expectedDaos);
            _mapperMock.Setup(x => x.Map<IEnumerable<CostingModel>>(expectedDaos))
                .Returns(expectedModels);

            // Act
            var result = await _costingService.GetListAsync(filter, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            _costingRepositoryMock.Verify(x => x.GetListAsync(filterDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingFilterDao>(filter), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<CostingModel>>(expectedDaos), Times.Once);
        }
    }
}