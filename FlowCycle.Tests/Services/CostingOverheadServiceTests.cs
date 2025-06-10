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
    public class CostingOverheadServiceTests
    {
        private Mock<ICostingOverheadRepository> _costingOverheadRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CostingOverheadService _costingOverheadService;
        private CancellationToken _defaultCancellationToken;

        [SetUp]
        public void Setup()
        {
            _costingOverheadRepositoryMock = new Mock<ICostingOverheadRepository>();
            _mapperMock = new Mock<IMapper>();
            _costingOverheadService = new CostingOverheadService(_costingOverheadRepositoryMock.Object, _mapperMock.Object);
            _defaultCancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task GetByIdAsync_WhenOverheadExists_ShouldReturnOverhead()
        {
            // Arrange
            var expectedOverhead = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200,
                Note = "Test note"
            };
            var expectedModel = new CostingOverhead
            {
                Id = 1,
                CostingId = 1,
                OverheadName = "Амортизация",
                OverheadType = "Прямой",
                CostValue = 200,
                Note = "Test note"
            };
            _costingOverheadRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ReturnsAsync(expectedOverhead);
            _mapperMock.Setup(x => x.Map<CostingOverhead>(expectedOverhead))
                .Returns(expectedModel);

            // Act
            var result = await _costingOverheadService.GetByIdAsync(1, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadName, Is.EqualTo("Амортизация"));
            Assert.That(result.OverheadType, Is.EqualTo("Прямой"));
            Assert.That(result.CostValue, Is.EqualTo(200));
            _costingOverheadRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverhead>(expectedOverhead), Times.Once);
        }

        [Test]
        public void GetByIdAsync_WhenOverheadDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _costingOverheadRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _costingOverheadService.GetByIdAsync(1, _defaultCancellationToken));
            _costingOverheadRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewOverhead()
        {
            // Arrange
            var overheadModel = new CostingOverhead
            {
                CostingId = 1,
                OverheadName = "Амортизация",
                OverheadType = "Прямой",
                CostValue = 200,
                Note = "Test note"
            };
            var overheadDao = new CostingOverheadDao
            {
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200,
                Note = "Test note"
            };
            var createdDao = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200,
                Note = "Test note"
            };
            var createdModel = new CostingOverhead
            {
                Id = 1,
                CostingId = 1,
                OverheadName = "Амортизация",
                OverheadType = "Прямой",
                CostValue = 200,
                Note = "Test note"
            };

            _mapperMock.Setup(x => x.Map<CostingOverheadDao>(overheadModel))
                .Returns(overheadDao);
            _costingOverheadRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CostingOverheadDao>(), _defaultCancellationToken))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(x => x.Map<CostingOverhead>(createdDao))
                .Returns(createdModel);

            // Act
            var result = await _costingOverheadService.CreateAsync(overheadModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostValue, Is.EqualTo(200));
            _costingOverheadRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingOverheadDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverheadDao>(overheadModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverhead>(createdDao), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenOverheadExists_ShouldUpdateOverhead()
        {
            // Arrange
            var overheadModel = new CostingOverhead
            {
                Id = 1,
                CostingId = 1,
                OverheadName = "Амортизация",
                OverheadType = "Прямой",
                CostValue = 300,
                Note = "Updated note"
            };
            var overheadDao = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 2,
                TotalValue = 300,
                Note = "Updated note"
            };
            var updatedDao = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 2,
                TotalValue = 300,
                Note = "Updated note"
            };
            var updatedModel = new CostingOverhead
            {
                Id = 1,
                CostingId = 1,
                OverheadName = "Амортизация",
                OverheadType = "Прямой",
                CostValue = 300,
                Note = "Updated note"
            };

            _mapperMock.Setup(x => x.Map<CostingOverheadDao>(overheadModel))
                .Returns(overheadDao);
            _costingOverheadRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<CostingOverheadDao>(), _defaultCancellationToken))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(x => x.Map<CostingOverhead>(updatedDao))
                .Returns(updatedModel);

            // Act
            var result = await _costingOverheadService.UpdateAsync(overheadModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostValue, Is.EqualTo(300));
            _costingOverheadRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CostingOverheadDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverheadDao>(overheadModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverhead>(updatedDao), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_WhenOverheadExists_ShouldDeleteOverhead()
        {
            // Arrange
            _costingOverheadRepositoryMock.Setup(x => x.DeleteAsync(1, _defaultCancellationToken))
                .Returns(Task.CompletedTask);

            // Act
            await _costingOverheadService.DeleteAsync(1, _defaultCancellationToken);

            // Assert
            _costingOverheadRepositoryMock.Verify(x => x.DeleteAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task GetListAsync_WithFilter_ShouldReturnFilteredOverheads()
        {
            // Arrange
            var filter = new CostingOverheadFilter { CostingId = 1 };
            var filterDao = new CostingOverheadFilterDao { CostingId = 1 };
            var expectedDaos = new List<CostingOverheadDao>
            {
                new CostingOverheadDao
                {
                    Id = 1,
                    CostingId = 1,
                    OverheadTypeId = 1,
                    Uom = "шт",
                    UnitPrice = 100,
                    QtyPerProduct = 2,
                    TotalValue = 200,
                    Note = "Test note"
                },
                new CostingOverheadDao
                {
                    Id = 2,
                    CostingId = 1,
                    OverheadTypeId = 2,
                    Uom = "кВт",
                    UnitPrice = 50,
                    QtyPerProduct = 3,
                    TotalValue = 150,
                    Note = "Test note"
                }
            };
            var expectedModels = new List<CostingOverhead>
            {
                new CostingOverhead
                {
                    Id = 1,
                    CostingId = 1,
                    OverheadName = "Амортизация",
                    OverheadType = "Прямой",
                    CostValue = 200,
                    Note = "Test note"
                },
                new CostingOverhead
                {
                    Id = 2,
                    CostingId = 1,
                    OverheadName = "Электроэнергия",
                    OverheadType = "Косвенный",
                    CostValue = 150,
                    Note = "Test note"
                }
            };

            _mapperMock.Setup(x => x.Map<CostingOverheadFilterDao>(filter))
                .Returns(filterDao);
            _costingOverheadRepositoryMock.Setup(x => x.GetListAsync(filterDao, _defaultCancellationToken))
                .ReturnsAsync(expectedDaos);
            _mapperMock.Setup(x => x.Map<IEnumerable<CostingOverhead>>(It.IsAny<IEnumerable<CostingOverheadDao>>()))
                .Returns(expectedModels);

            // Act
            var result = await _costingOverheadService.GetListAsync(filter, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            var resultList = result.ToList();
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].OverheadName, Is.EqualTo("Амортизация"));
            Assert.That(resultList[1].Id, Is.EqualTo(2));
            Assert.That(resultList[1].OverheadName, Is.EqualTo("Электроэнергия"));
            _costingOverheadRepositoryMock.Verify(x => x.GetListAsync(filterDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverheadFilterDao>(filter), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<CostingOverhead>>(It.IsAny<IEnumerable<CostingOverheadDao>>()), Times.Once);
        }
    }
}