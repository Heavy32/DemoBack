using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
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
    public class CostingOverheadRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;
        private AppDbContext _context;
        private CostingOverheadRepository _repository;
        private CancellationToken _defaultCancellationToken;
        private string _databaseName;

        [SetUp]
        public void Setup()
        {
            _databaseName = Guid.NewGuid().ToString();
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .Options;

            _context = new AppDbContext(_options);
            _repository = new CostingOverheadRepository(_context);
            _defaultCancellationToken = CancellationToken.None;

            // Seed test data
            var costing = new CostingDao
            {
                Id = 100,
                ProductName = "Test Product",
                ProductCode = "TP001",
                CostingTypeId = 1,
                Uom = "шт",
                Quantity = 1,
                UnitCost = 100,
                TotalCost = 100,
                ProjectId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var overheadType = new OverheadTypeDao
            {
                Id = 1,
                Name = "Прямой"
            };

            var overhead1 = new CostingOverheadDao
            {
                Id = 100,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "шт",
                UnitPrice = 100,
                QtyPerProduct = 2,
                TotalValue = 200,
                Note = "Test note 1"
            };

            var overhead2 = new CostingOverheadDao
            {
                Id = 2,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "кВт",
                UnitPrice = 50,
                QtyPerProduct = 3,
                TotalValue = 150,
                Note = "Test note 2"
            };

            _context.Database.EnsureCreated();
            _context.Costings.Add(costing);
            _context.OverheadTypes.Add(overheadType);
            _context.CostingOverheads.Add(overhead1);
            _context.CostingOverheads.Add(overhead2);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_WhenOverheadExists_ShouldReturnOverhead()
        {
            // Act
            var result = await _repository.GetByIdAsync(1, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("шт"));
            Assert.That(result.UnitPrice, Is.EqualTo(100));
            Assert.That(result.QtyPerProduct, Is.EqualTo(2));
            Assert.That(result.TotalValue, Is.EqualTo(200));
            Assert.That(result.Note, Is.EqualTo("Test note 1"));
        }

        [Test]
        public void GetByIdAsync_WhenOverheadDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetByIdAsync(999, _defaultCancellationToken));
        }

        [Test]
        public async Task GetListAsync_WithFilter_ShouldReturnFilteredOverheads()
        {
            // Arrange
            var filter = new CostingOverheadFilterDao { CostingId = 1 };

            // Act
            var result = await _repository.GetListAsync(filter, _defaultCancellationToken);

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
        }

        [Test]
        public async Task GetListAsync_WithOverheadTypeFilter_ShouldReturnFilteredOverheads()
        {
            // Arrange
            var filter = new CostingOverheadFilterDao { OverheadTypeId = 1 };

            // Act
            var result = await _repository.GetListAsync(filter, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            var resultList = result.ToList();
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].OverheadTypeId, Is.EqualTo(1));
            Assert.That(resultList[1].Id, Is.EqualTo(2));
            Assert.That(resultList[1].OverheadTypeId, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewOverhead()
        {
            // Arrange
            var overhead = new CostingOverheadDao
            {
                OverheadTypeId = 1,
                Uom = "л",
                UnitPrice = 75,
                QtyPerProduct = 4,
                TotalValue = 300,
                Note = "New test note"
            };

            // Act
            var result = await _repository.CreateAsync(overhead, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.GreaterThan(0));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("л"));
            Assert.That(result.UnitPrice, Is.EqualTo(75));
            Assert.That(result.QtyPerProduct, Is.EqualTo(4));
            Assert.That(result.TotalValue, Is.EqualTo(300));
            Assert.That(result.Note, Is.EqualTo("New test note"));

            // Verify it was saved to the database
            var savedOverhead = await _context.CostingOverheads.FindAsync(result.Id);
            Assert.That(savedOverhead, Is.Not.Null);
            Assert.That(savedOverhead.Uom, Is.EqualTo("л"));
            Assert.That(savedOverhead.UnitPrice, Is.EqualTo(75));
            Assert.That(savedOverhead.QtyPerProduct, Is.EqualTo(4));
            Assert.That(savedOverhead.TotalValue, Is.EqualTo(300));
            Assert.That(savedOverhead.Note, Is.EqualTo("New test note"));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateExistingOverhead()
        {
            // Arrange
            var overhead = new CostingOverheadDao
            {
                Id = 1,
                CostingId = 1,
                OverheadTypeId = 1,
                Uom = "м³",
                UnitPrice = 200,
                QtyPerProduct = 5,
                TotalValue = 1000,
                Note = "Updated test note"
            };

            // Act
            var result = await _repository.UpdateAsync(overhead, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.OverheadTypeId, Is.EqualTo(1));
            Assert.That(result.Uom, Is.EqualTo("м³"));
            Assert.That(result.UnitPrice, Is.EqualTo(200));
            Assert.That(result.QtyPerProduct, Is.EqualTo(5));
            Assert.That(result.TotalValue, Is.EqualTo(1000));
            Assert.That(result.Note, Is.EqualTo("Updated test note"));

            // Verify it was updated in the database
            var updatedOverhead = await _context.CostingOverheads.FindAsync(1);
            Assert.That(updatedOverhead, Is.Not.Null);
            Assert.That(updatedOverhead.Uom, Is.EqualTo("м³"));
            Assert.That(updatedOverhead.UnitPrice, Is.EqualTo(200));
            Assert.That(updatedOverhead.QtyPerProduct, Is.EqualTo(5));
            Assert.That(updatedOverhead.TotalValue, Is.EqualTo(1000));
            Assert.That(updatedOverhead.Note, Is.EqualTo("Updated test note"));
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteOverhead()
        {
            // Act
            await _repository.DeleteAsync(1, _defaultCancellationToken);

            // Assert
            var deletedOverhead = await _context.CostingOverheads.FindAsync(1);
            Assert.That(deletedOverhead, Is.Null);
        }
    }
}