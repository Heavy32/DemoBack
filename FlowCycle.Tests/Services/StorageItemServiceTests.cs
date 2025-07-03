using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Domain.Storage.Models;
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
    public class StorageItemServiceTests
    {
        private Mock<IStorageItemRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<ISupplierRepository> _supplierRepositoryMock;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private StorageItemService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IStorageItemRepository>();
            _mapperMock = new Mock<IMapper>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _supplierRepositoryMock = new Mock<ISupplierRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _service = new StorageItemService(
                _repositoryMock.Object,
                _mapperMock.Object,
                _categoryRepositoryMock.Object,
                _supplierRepositoryMock.Object,
                _projectRepositoryMock.Object
            );
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ReturnsMappedItem()
        {
            // Arrange
            var dao = new StorageItemDao { Id = 1, Name = "Test Item" };
            var domain = new StorageItem { Id = 1, Name = "Test Item" };

            _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(dao);
            _mapperMock.Setup(m => m.Map<StorageItem>(dao))
                .Returns(domain);

            // Act
            var result = await _service.GetByIdAsync(1, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test Item"));
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StorageItemDao)null);

            // Act
            var result = await _service.GetByIdAsync(999, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllMappedItems()
        {
            // Arrange
            var daos = new List<StorageItemDao>
            {
                new StorageItemDao { Id = 1, Name = "Item 1" },
                new StorageItemDao { Id = 2, Name = "Item 2" }
            };

            var domains = new List<StorageItem>
            {
                new StorageItem { Id = 1, Name = "Item 1" },
                new StorageItem { Id = 2, Name = "Item 2" }
            };

            _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(daos);
            _mapperMock.Setup(m => m.Map<IEnumerable<StorageItem>>(daos))
                .Returns(domains);

            // Act
            var result = await _service.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task Create_ValidItem_CreatesAndReturnsMappedItem()
        {
            // Arrange
            var domain = new StorageItem { Name = "New Item" };
            var dao = new StorageItemDao { Name = "New Item" };
            var createdDao = new StorageItemDao { Id = 1, Name = "New Item" };
            var createdDomain = new StorageItem { Id = 1, Name = "New Item" };

            _mapperMock.Setup(m => m.Map<StorageItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.CreateAsync(dao, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(m => m.Map<StorageItem>(createdDao))
                .Returns(createdDomain);

            // Act
            var result = await _service.CreateAsync(domain, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("New Item"));
        }

        [Test]
        public async Task UpdateAsync_ExistingItem_UpdatesAndReturnsMappedItem()
        {
            // Arrange
            var domain = new StorageItem { Id = 1, Name = "Updated Item" };
            var dao = new StorageItemDao { Id = 1, Name = "Updated Item" };
            var updatedDao = new StorageItemDao { Id = 1, Name = "Updated Item" };
            var updatedDomain = new StorageItem { Id = 1, Name = "Updated Item" };

            _mapperMock.Setup(m => m.Map<StorageItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.UpdateAsync(dao, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(m => m.Map<StorageItem>(updatedDao))
                .Returns(updatedDomain);

            // Act
            var result = await _service.UpdateAsync(domain, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Updated Item"));
        }

        [Test]
        public async Task UpdateAsync_NonExistingItem_ReturnsNull()
        {
            // Arrange
            var domain = new StorageItem { Id = 999, Name = "Non Existing" };
            var dao = new StorageItemDao { Id = 999, Name = "Non Existing" };

            _mapperMock.Setup(m => m.Map<StorageItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.UpdateAsync(dao, It.IsAny<CancellationToken>()))
                .ReturnsAsync((StorageItemDao)null);

            // Act
            var result = await _service.UpdateAsync(domain, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_ExistingItem_DeletesItem()
        {
            // Arrange
            var domain = new StorageItem { Id = 1, Name = "Test Item" };
            var dao = new StorageItemDao { Id = 1, Name = "Test Item" };

            _mapperMock.Setup(m => m.Map<StorageItemDao>(domain))
                .Returns(dao);
            _repositoryMock.Setup(r => r.DeleteAsync(domain.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _service.DeleteAsync(domain.Id, CancellationToken.None));
        }
    }
}