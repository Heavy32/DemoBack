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
using System.Linq;

namespace FlowCycle.Tests.Services
{
    [TestFixture]
    public class CostingMaterialServiceTests
    {
        private Mock<ICostingMaterialRepository> _costingMaterialRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CostingMaterialService _costingMaterialService;
        private CancellationToken _defaultCancellationToken;

        [SetUp]
        public void Setup()
        {
            _costingMaterialRepositoryMock = new Mock<ICostingMaterialRepository>();
            _mapperMock = new Mock<IMapper>();
            _costingMaterialService = new CostingMaterialService(_costingMaterialRepositoryMock.Object, _mapperMock.Object);
            _defaultCancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task GetByIdAsync_WhenMaterialExists_ShouldReturnMaterial()
        {
            // Arrange
            var expectedMaterial = new CostingMaterialDao
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200
            };
            var expectedModel = new CostingMaterial
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200
            };
            _costingMaterialRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ReturnsAsync(expectedMaterial);
            _mapperMock.Setup(x => x.Map<CostingMaterial>(expectedMaterial))
                .Returns(expectedModel);

            // Act
            var result = await _costingMaterialService.GetByIdAsync(1, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.CostingMaterialTypeId, Is.EqualTo(1));
            Assert.That(result.TotalValue, Is.EqualTo(200));
            _costingMaterialRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingMaterial>(expectedMaterial), Times.Once);
        }

        [Test]
        public void GetByIdAsync_WhenMaterialDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _costingMaterialRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _costingMaterialService.GetByIdAsync(1, _defaultCancellationToken));
            _costingMaterialRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewMaterial()
        {
            // Arrange
            var materialModel = new CostingMaterial
            {
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200
            };
            var materialDao = new CostingMaterialDao
            {
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200
            };
            var createdDao = new CostingMaterialDao
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200
            };
            var createdModel = new CostingMaterial
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200
            };

            _mapperMock.Setup(x => x.Map<CostingMaterialDao>(materialModel))
                .Returns(materialDao);
            _costingMaterialRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CostingMaterialDao>(), _defaultCancellationToken))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(x => x.Map<CostingMaterial>(createdDao))
                .Returns(createdModel);

            // Act
            var result = await _costingMaterialService.CreateAsync(materialModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.CostingMaterialTypeId, Is.EqualTo(1));
            Assert.That(result.TotalValue, Is.EqualTo(200));
            _costingMaterialRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingMaterialDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingMaterialDao>(materialModel), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenMaterialExists_ShouldUpdateMaterial()
        {
            // Arrange
            var materialModel = new CostingMaterial
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450
            };
            var materialDao = new CostingMaterialDao
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450
            };
            var updatedDao = new CostingMaterialDao
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450
            };
            var updatedModel = new CostingMaterial
            {
                Id = 1,
                CostingId = 1,
                CostingMaterialTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450
            };

            _mapperMock.Setup(x => x.Map<CostingMaterialDao>(materialModel))
                .Returns(materialDao);
            _costingMaterialRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<CostingMaterialDao>(), _defaultCancellationToken))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(x => x.Map<CostingMaterial>(updatedDao))
                .Returns(updatedModel);

            // Act
            var result = await _costingMaterialService.UpdateAsync(materialModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.CostingMaterialTypeId, Is.EqualTo(1));
            Assert.That(result.TotalValue, Is.EqualTo(450));
            _costingMaterialRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CostingMaterialDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingMaterialDao>(materialModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingMaterial>(updatedDao), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_WhenMaterialExists_ShouldDeleteMaterial()
        {
            // Arrange
            _costingMaterialRepositoryMock.Setup(x => x.DeleteAsync(1, _defaultCancellationToken))
                .Returns(Task.CompletedTask);

            // Act
            await _costingMaterialService.DeleteAsync(1, _defaultCancellationToken);

            // Assert
            _costingMaterialRepositoryMock.Verify(x => x.DeleteAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task GetListAsync_WithFilter_ShouldReturnFilteredMaterials()
        {
            // Arrange
            var filter = new CostingMaterialFilter { CostingId = 1 };
            var filterDao = new CostingMaterialFilterDao { CostingId = 1 };
            var expectedDaos = new List<CostingMaterialDao>
            {
                new CostingMaterialDao { Id = 1, CostingId = 1, CostingMaterialTypeId = 1 },
                new CostingMaterialDao { Id = 2, CostingId = 1, CostingMaterialTypeId = 2 }
            };
            var expectedModels = new List<CostingMaterial>
            {
                new CostingMaterial { Id = 1, CostingId = 1, CostingMaterialTypeId = 1 },
                new CostingMaterial { Id = 2, CostingId = 1, CostingMaterialTypeId = 2 }
            };

            _mapperMock.Setup(x => x.Map<CostingMaterialFilterDao>(filter))
                .Returns(filterDao);
            _costingMaterialRepositoryMock.Setup(x => x.GetListAsync(filterDao, _defaultCancellationToken))
                .ReturnsAsync(expectedDaos);
            _mapperMock.Setup(x => x.Map<IEnumerable<CostingMaterial>>(It.IsAny<IEnumerable<CostingMaterialDao>>()))
                .Returns(expectedModels);

            // Act
            var result = await _costingMaterialService.GetListAsync(filter, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            _costingMaterialRepositoryMock.Verify(x => x.GetListAsync(filterDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingMaterialFilterDao>(filter), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<CostingMaterial>>(It.IsAny<IEnumerable<CostingMaterialDao>>()), Times.Once);
        }
    }
}