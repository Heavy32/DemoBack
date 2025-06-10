using AutoMapper;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowCycle.Tests.Services
{
    [TestFixture]
    public class CostingOverheadServiceTests
    {
        private Mock<ICostingOverheadRepository> _costingOverheadRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CostingOverheadService _service;
        private CancellationToken _defaultCancellationToken;

        [SetUp]
        public void Setup()
        {
            _costingOverheadRepositoryMock = new Mock<ICostingOverheadRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new CostingOverheadService(_costingOverheadRepositoryMock.Object, _mapperMock.Object);
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
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200,
                Note = "Test note"
            };

            _costingOverheadRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ReturnsAsync(expectedOverhead);
            _mapperMock.Setup(x => x.Map<CostingOverhead>(expectedOverhead))
                .Returns(expectedModel);

            // Act
            var result = await _service.GetByIdAsync(1, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("шт"));
            Assert.That(result.UnitPrice, Is.EqualTo(100));
            Assert.That(result.QtyPerProduct, Is.EqualTo(2));
            Assert.That(result.TotalValue, Is.EqualTo(200));
            Assert.That(result.Note, Is.EqualTo("Test note"));
            _costingOverheadRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverhead>(expectedOverhead), Times.Once);
        }

        [Test]
        public void GetByIdAsync_WhenOverheadDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _costingOverheadRepositoryMock.Setup(x => x.GetByIdAsync(999, _defaultCancellationToken))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(999, _defaultCancellationToken));
            _costingOverheadRepositoryMock.Verify(x => x.GetByIdAsync(999, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewOverhead()
        {
            // Arrange
            var overhead = new CostingOverhead
            {
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200,
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

            var expectedModel = new CostingOverhead
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

            _mapperMock.Setup(x => x.Map<CostingOverheadDao>(overhead))
                .Returns(overheadDao);
            _costingOverheadRepositoryMock.Setup(x => x.CreateAsync(overheadDao, _defaultCancellationToken))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(x => x.Map<CostingOverhead>(createdDao))
                .Returns(expectedModel);

            // Act
            var result = await _service.CreateAsync(overhead, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("шт"));
            Assert.That(result.UnitPrice, Is.EqualTo(100));
            Assert.That(result.QtyPerProduct, Is.EqualTo(2));
            Assert.That(result.TotalValue, Is.EqualTo(200));
            Assert.That(result.Note, Is.EqualTo("Test note"));
            _mapperMock.Verify(x => x.Map<CostingOverheadDao>(overhead), Times.Once);
            _costingOverheadRepositoryMock.Verify(x => x.CreateAsync(overheadDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverhead>(createdDao), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateExistingOverhead()
        {
            // Arrange
            var overhead = new CostingOverhead
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450,
                Note = "Updated note"
            };

            var overheadDao = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450,
                Note = "Updated note"
            };

            var updatedDao = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450,
                Note = "Updated note"
            };

            var expectedModel = new CostingOverhead
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 150,
                QtyPerProduct = 3,
                TotalValue = 450,
                Note = "Updated note"
            };

            _mapperMock.Setup(x => x.Map<CostingOverheadDao>(overhead))
                .Returns(overheadDao);
            _costingOverheadRepositoryMock.Setup(x => x.UpdateAsync(overheadDao, _defaultCancellationToken))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(x => x.Map<CostingOverhead>(updatedDao))
                .Returns(expectedModel);

            // Act
            var result = await _service.UpdateAsync(overhead, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("шт"));
            Assert.That(result.UnitPrice, Is.EqualTo(150));
            Assert.That(result.QtyPerProduct, Is.EqualTo(3));
            Assert.That(result.TotalValue, Is.EqualTo(450));
            Assert.That(result.Note, Is.EqualTo("Updated note"));
            _mapperMock.Verify(x => x.Map<CostingOverheadDao>(overhead), Times.Once);
            _costingOverheadRepositoryMock.Verify(x => x.UpdateAsync(overheadDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingOverhead>(updatedDao), Times.Once);
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
                    Note = "Test note 1"
                },
                new CostingOverheadDao
                {
                    Id = 2,
                    CostingId = 1,
                    OverheadTypeId = 1,
                    Uom = "кВт",
                    UnitPrice = 50,
                    QtyPerProduct = 3,
                    TotalValue = 150,
                    Note = "Test note 2"
                }
            };

            var expectedModels = new List<CostingOverhead>
            {
                new CostingOverhead
                {
                    Id = 1,
                    CostingId = 1,
                    OverheadTypeId = 1,
                    Uom = "шт",
                    UnitPrice = 100,
                    QtyPerProduct = 2,
                    TotalValue = 200,
                    Note = "Test note 1"
                },
                new CostingOverhead
                {
                    Id = 2,
                    CostingId = 1,
                    OverheadTypeId = 1,
                    Uom = "кВт",
                    UnitPrice = 50,
                    QtyPerProduct = 3,
                    TotalValue = 150,
                    Note = "Test note 2"
                }
            };

            _mapperMock.Setup(x => x.Map<CostingOverheadFilterDao>(filter))
                .Returns(filterDao);
            _costingOverheadRepositoryMock.Setup(x => x.GetListAsync(filterDao, _defaultCancellationToken))
                .ReturnsAsync(expectedDaos);
            _mapperMock.Setup(x => x.Map<IEnumerable<CostingOverhead>>(expectedDaos))
                .Returns(expectedModels);

            // Act
            var result = await _service.GetListAsync(filter, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            var resultList = result.ToList();
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].Uom, Is.EqualTo("шт"));
            Assert.That(resultList[0].UnitPrice, Is.EqualTo(100));
            Assert.That(resultList[0].QtyPerProduct, Is.EqualTo(2));
            Assert.That(resultList[0].TotalValue, Is.EqualTo(200));
            Assert.That(resultList[1].Id, Is.EqualTo(2));
            Assert.That(resultList[1].Uom, Is.EqualTo("кВт"));
            Assert.That(resultList[1].UnitPrice, Is.EqualTo(50));
            Assert.That(resultList[1].QtyPerProduct, Is.EqualTo(3));
            Assert.That(resultList[1].TotalValue, Is.EqualTo(150));
            _mapperMock.Verify(x => x.Map<CostingOverheadFilterDao>(filter), Times.Once);
            _costingOverheadRepositoryMock.Verify(x => x.GetListAsync(filterDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<CostingOverhead>>(expectedDaos), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteOverhead()
        {
            // Act
            await _service.DeleteAsync(1, _defaultCancellationToken);

            // Assert
            _costingOverheadRepositoryMock.Verify(x => x.DeleteAsync(1, _defaultCancellationToken), Times.Once);
        }
    }
}