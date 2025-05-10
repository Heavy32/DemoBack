using AutoMapper;
using FlowCycle.Domain.Stock;
using FlowCycle.Domain.Stock.Models;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;
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
    public class StockItemServiceTests
    {
        private Mock<IStockItemRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private StorageItemService _service;

        [SetUp]
        public void Setup()
        {   

            _repositoryMock = new Mock<IStockItemRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new StorageItemService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetById_ExistingId_ReturnsMappedItem()
        {
            // Arrange
            var dao = new StockItemDao { Id = 1, Name = "Test Item" };
            var domain = new StockItem { Id = 1, Name = "Test Item" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(dao);
            _mapperMock.Setup(m => m.Map<StockItem>(dao))
                .Returns(domain);

            // Act
            var result = await _service.GetById(1, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test Item"));
        }

        [Test]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StockItemDao)null);

            // Act
            var result = await _service.GetById(999, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetList_NoFilter_ReturnsAllMappedItems()
        {
            // Arrange
            var daos = new List<StockItemDao>
            {
                new StockItemDao { Id = 1, Name = "Item 1" },
                new StockItemDao { Id = 2, Name = "Item 2" }
            };

            var domains = new List<StockItem>
            {
                new StockItem { Id = 1, Name = "Item 1" },
                new StockItem { Id = 2, Name = "Item 2" }
            };

            _repositoryMock.Setup(r => r.GetListAsync(null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(daos);
            _mapperMock.Setup(m => m.Map<IEnumerable<StockItem>>(daos))
                .Returns(domains);

            // Act
            var result = await _service.GetList(null, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetList_WithFilter_ReturnsFilteredMappedItems()
        {
            // Arrange
            var filter = new StockItemFilter { Name = "Test" };
            var filterDao = new StockItemFilterDao { Name = "Test" };
            var daos = new List<StockItemDao>
            {
                new StockItemDao { Id = 1, Name = "Test Item" }
            };

            var domains = new List<StockItem>
            {
                new StockItem { Id = 1, Name = "Test Item" }
            };

            _mapperMock.Setup(m => m.Map<StockItemFilterDao>(filter))
                .Returns(filterDao);
            _repositoryMock.Setup(r => r.GetListAsync(It.Is<StockItemFilterDao>(f => f.Name == "Test"), It.IsAny<CancellationToken>()))
                .ReturnsAsync(daos);
            _mapperMock.Setup(m => m.Map<IEnumerable<StockItem>>(It.IsAny<IEnumerable<StockItemDao>>()))
                .Returns(domains);

            // Act
            var result = (await _service.GetList(filter, CancellationToken.None)).ToArray();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Test Item"));
        }

        [Test]
        public async Task Create_ValidItem_CreatesAndReturnsMappedItem()
        {
            // Arrange
            var domain = new StockItem { Name = "New Item" };
            var dao = new StockItemDao { Name = "New Item" };
            var createdDao = new StockItemDao { Id = 1, Name = "New Item" };
            var createdDomain = new StockItem { Id = 1, Name = "New Item" };

            _mapperMock.Setup(m => m.Map<StockItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.CreateAsync(dao, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(m => m.Map<StockItem>(createdDao))
                .Returns(createdDomain);

            // Act
            var result = await _service.Create(domain, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("New Item"));
        }

        [Test]
        public async Task Update_ExistingItem_UpdatesAndReturnsMappedItem()
        {
            // Arrange
            var domain = new StockItem { Id = 1, Name = "Updated Item" };
            var dao = new StockItemDao { Id = 1, Name = "Updated Item" };
            var updatedDao = new StockItemDao { Id = 1, Name = "Updated Item" };
            var updatedDomain = new StockItem { Id = 1, Name = "Updated Item" };

            _mapperMock.Setup(m => m.Map<StockItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.UpdateAsync(dao, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(m => m.Map<StockItem>(updatedDao))
                .Returns(updatedDomain);

            // Act
            var result = await _service.Update(domain, domain.Id, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Updated Item"));
        }

        [Test]
        public async Task Update_NonExistingItem_ReturnsNull()
        {
            // Arrange
            var domain = new StockItem { Id = 999, Name = "Non Existing" };
            var dao = new StockItemDao { Id = 999, Name = "Non Existing" };

            _mapperMock.Setup(m => m.Map<StockItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.UpdateAsync(dao, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StockItemDao)null);

            // Act
            var result = await _service.Update(domain, domain.Id, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Delete_ExistingItem_DeletesItem()
        {
            // Arrange
            var domain = new StockItem { Id = 1, Name = "Test Item" };
            var dao = new StockItemDao { Id = 1, Name = "Test Item" };

            _mapperMock.Setup(m => m.Map<StockItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.DeleteAsync(dao, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _service.Delete(domain, CancellationToken.None));
        }
    }
} 