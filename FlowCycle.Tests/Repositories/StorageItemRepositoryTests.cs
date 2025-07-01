using FlowCycle.Persistance;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowCycle.Tests.Repositories
{
    [TestFixture]
    public class StorageItemRepositoryTests
    {
        private AppDbContext _context;
        private StorageItemRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new StorageItemRepository(_context);

            // Seed test data
            var supplier = new SupplierDao { Id = 1, Name = "Test Supplier" };
            var category = new CategoryDao { Id = 1, Name = "Test Category" };
            var project = new ProjectDao { Id = 1, Name = "Test Project" };

            _context.Suppliers.Add(supplier);
            _context.Categories.Add(category);
            _context.Projects.Add(project);

            var items = new List<StorageItemDao>
            {
                new StorageItemDao { Id = 1, Name = "Item 1", Code = "CODE1", Supplier = supplier, Category = category, Project = project, SinglePrice = 100, Amount = 10, Measure = "Unit" },
                new StorageItemDao { Id = 2, Name = "Item 2", Code = "CODE2", Supplier = supplier, Category = category, Project = project, SinglePrice = 200, Amount = 20, Measure = "Unit" },
                new StorageItemDao { Id = 3, Name = "Item 3", Code = "CODE3", Supplier = supplier, Category = category, Project = project, SinglePrice = 300, Amount = 30, Measure = "Unit" }
            };

            _context.Storages.AddRange(items);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ReturnsCorrectItem()
        {
            // Act
            var result = await _repository.GetByIdAsync(1, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Item 1"));
        }

        [Test]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Act
            var result = await _repository.GetByIdAsync(999, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetListAsync_NoFilter_ReturnsAllItems()
        {
            // Act
            var result = await _repository.GetListAsync(null, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetListAsync_WithNameFilter_ReturnsFilteredItems()
        {
            // Arrange
            var filter = new StorageItemFilterDao { Name = "Item 1" };

            // Act
            var result = await _repository.GetListAsync(filter, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Item 1"));
        }

        [Test]
        public async Task GetListAsync_WithSupplierFilter_ReturnsFilteredItems()
        {
            // Arrange
            var filter = new StorageItemFilterDao { Supplier = "Test" };

            // Act
            var result = await _repository.GetListAsync(filter, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetListAsync_WithSorting_ReturnsSortedItems()
        {
            // Arrange
            var filter = new StorageItemFilterDao { SortColumn = "SinglePrice", SortDescending = true };

            // Act
            var result = await _repository.GetListAsync(filter, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().SinglePrice, Is.EqualTo(300));
            Assert.That(result.Last().SinglePrice, Is.EqualTo(100));
        }

        [Test]
        public async Task CreateAsync_ValidItem_CreatesAndReturnsItem()
        {
            // Arrange
            var newItem = new StorageItemDao
            {
                Name = "New Item",
                Code = "NEW1",
                Supplier = _context.Suppliers.First(),
                Category = _context.Categories.First(),
                Project = _context.Projects.First(),
                SinglePrice = 400,
                Amount = 40,
                Measure = "Unit"
            };

            // Act
            var result = await _repository.CreateAsync(newItem, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.GreaterThan(0));
            Assert.That(result.Name, Is.EqualTo("New Item"));

            var savedItem = await _context.Storages.FindAsync(result.Id);
            Assert.That(savedItem, Is.Not.Null);
            Assert.That(savedItem.Name, Is.EqualTo("New Item"));
        }

        [Test]
        public async Task UpdateAsync_ExistingItem_UpdatesAndReturnsItem()
        {
            // Arrange
            var item = await _context.Storages.FindAsync(1);
            item.Name = "Updated Item";

            // Act
            var result = await _repository.UpdateAsync(item, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Updated Item"));

            var updatedItem = await _context.Storages.FindAsync(1);
            Assert.That(updatedItem.Name, Is.EqualTo("Updated Item"));
        }

        [Test]
        public async Task UpdateAsync_NonExistingItem_ReturnsNull()
        {
            // Arrange
            var item = new StorageItemDao { Id = 999, Name = "Non Existing" };

            // Act
            var result = await _repository.UpdateAsync(item, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_ExistingItem_RemovesItem()
        {
            // Arrange
            var item = await _context.Storages.FindAsync(1);

            // Act
            await _repository.DeleteAsync(item.Id, CancellationToken.None);

            // Assert
            var deletedItem = await _context.Storages.FindAsync(1);
            Assert.That(deletedItem, Is.Null);
        }
    }
}