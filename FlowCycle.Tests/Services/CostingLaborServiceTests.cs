using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.UnitOfWork;
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
    public class CostingLaborServiceTests
    {
        private Mock<ICostingLaborRepository> _costingLaborRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CostingLaborService _costingLaborService;
        private CancellationToken _defaultCancellationToken;

        [SetUp]
        public void Setup()
        {
            _costingLaborRepositoryMock = new Mock<ICostingLaborRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _costingLaborService = new CostingLaborService(_costingLaborRepositoryMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);
            _defaultCancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task GetByIdAsync_WhenLaborExists_ShouldReturnLabor()
        {
            // Arrange
            var expectedLabor = new CostingLaborDao
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 2,
                HourRate = 1000,
                TotalValue = 2000,
                Note = "Test note"
            };
            var expectedModel = new CostingLabor
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 2,
                HourRate = 1000,
                TotalValue = 2000,
                Note = "Test note"
            };
            _costingLaborRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ReturnsAsync(expectedLabor);
            _mapperMock.Setup(x => x.Map<CostingLabor>(expectedLabor))
                .Returns(expectedModel);

            // Act
            var result = await _costingLaborService.GetByIdAsync(1, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.CostingId, Is.EqualTo(1));
            Assert.That(result.LaborName, Is.EqualTo("Сборка"));
            Assert.That(result.Hours, Is.EqualTo(2));
            Assert.That(result.HourRate, Is.EqualTo(1000));
            Assert.That(result.TotalValue, Is.EqualTo(2000));
            _costingLaborRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingLabor>(expectedLabor), Times.Once);
        }

        [Test]
        public void GetByIdAsync_WhenLaborDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _costingLaborRepositoryMock.Setup(x => x.GetByIdAsync(1, _defaultCancellationToken))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _costingLaborService.GetByIdAsync(1, _defaultCancellationToken));
            _costingLaborRepositoryMock.Verify(x => x.GetByIdAsync(1, _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewLabor()
        {
            // Arrange
            var laborModel = new CostingLabor
            {
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 2,
                HourRate = 1000,
                TotalValue = 2000,
                Note = "Test note"
            };
            var laborDao = new CostingLaborDao
            {
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 2,
                HourRate = 1000,
                TotalValue = 2000,
                Note = "Test note"
            };
            var createdDao = new CostingLaborDao
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 2,
                HourRate = 1000,
                TotalValue = 2000,
                Note = "Test note"
            };
            var createdModel = new CostingLabor
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 2,
                HourRate = 1000,
                TotalValue = 2000,
                Note = "Test note"
            };

            _mapperMock.Setup(x => x.Map<CostingLaborDao>(laborModel))
                .Returns(laborDao);
            _costingLaborRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CostingLaborDao>(), _defaultCancellationToken))
                .ReturnsAsync(createdDao);
            _mapperMock.Setup(x => x.Map<CostingLabor>(createdDao))
                .Returns(createdModel);

            // Act
            var result = await _costingLaborService.CreateAsync(laborModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.TotalValue, Is.EqualTo(2000));
            _costingLaborRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingLaborDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingLaborDao>(laborModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingLabor>(createdDao), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenLaborExists_ShouldUpdateLabor()
        {
            // Arrange
            var laborModel = new CostingLabor
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 3,
                HourRate = 1500,
                TotalValue = 4500,
                Note = "Updated note"
            };
            var laborDao = new CostingLaborDao
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 3,
                HourRate = 1500,
                TotalValue = 4500,
                Note = "Updated note"
            };
            var updatedDao = new CostingLaborDao
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 3,
                HourRate = 1500,
                TotalValue = 4500,
                Note = "Updated note"
            };
            var updatedModel = new CostingLabor
            {
                Id = 1,
                CostingId = 1,
                LaborName = "Сборка",
                Hours = 3,
                HourRate = 1500,
                TotalValue = 4500,
                Note = "Updated note"
            };

            _mapperMock.Setup(x => x.Map<CostingLaborDao>(laborModel))
                .Returns(laborDao);
            _costingLaborRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<CostingLaborDao>(), _defaultCancellationToken))
                .ReturnsAsync(updatedDao);
            _mapperMock.Setup(x => x.Map<CostingLabor>(updatedDao))
                .Returns(updatedModel);

            // Act
            var result = await _costingLaborService.UpdateAsync(laborModel, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.TotalValue, Is.EqualTo(4500));
            _costingLaborRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CostingLaborDao>(), _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingLaborDao>(laborModel), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingLabor>(updatedDao), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_WhenLaborExists_ShouldDeleteLabor()
        {
            // Arrange
            _costingLaborRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<CostingLaborDao>(), _defaultCancellationToken))
                .Returns(Task.CompletedTask);

            // Act
            await _costingLaborService.DeleteAsync(1, _defaultCancellationToken);

            // Assert
            _costingLaborRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<CostingLaborDao>(), _defaultCancellationToken), Times.Once);
        }

        [Test]
        public async Task GetListAsync_WithFilter_ShouldReturnFilteredLabors()
        {
            // Arrange
            var filter = new CostingLaborFilter { CostingId = 1 };
            var filterDao = new CostingLaborFilterDao { CostingId = 1 };
            var expectedDaos = new List<CostingLaborDao>
            {
                new CostingLaborDao
                {
                    Id = 1,
                    CostingId = 1,
                    LaborName = "Сборка",
                    Hours = 2,
                    HourRate = 1000,
                    TotalValue = 2000,
                    Note = "Test note"
                },
                new CostingLaborDao
                {
                    Id = 2,
                    CostingId = 1,
                    LaborName = "Покраска",
                    Hours = 3,
                    HourRate = 1500,
                    TotalValue = 4500,
                    Note = "Test note"
                }
            };
            var expectedModels = new List<CostingLabor>
            {
                new CostingLabor
                {
                    Id = 1,
                    CostingId = 1,
                    LaborName = "Сборка",
                    Hours = 2,
                    HourRate = 1000,
                    TotalValue = 2000,
                    Note = "Test note"
                },
                new CostingLabor
                {
                    Id = 2,
                    CostingId = 1,
                    LaborName = "Покраска",
                    Hours = 3,
                    HourRate = 1500,
                    TotalValue = 4500,
                    Note = "Test note"
                }
            };

            _mapperMock.Setup(x => x.Map<CostingLaborFilterDao>(filter))
                .Returns(filterDao);
            _costingLaborRepositoryMock.Setup(x => x.GetListAsync(filterDao, _defaultCancellationToken))
                .ReturnsAsync(expectedDaos);
            _mapperMock.Setup(x => x.Map<IEnumerable<CostingLabor>>(It.IsAny<IEnumerable<CostingLaborDao>>()))
                .Returns(expectedModels);

            // Act
            var result = await _costingLaborService.GetListAsync(filter, _defaultCancellationToken);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            var resultList = result.ToList();
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].LaborName, Is.EqualTo("Сборка"));
            Assert.That(resultList[1].Id, Is.EqualTo(2));
            Assert.That(resultList[1].LaborName, Is.EqualTo("Покраска"));
            _costingLaborRepositoryMock.Verify(x => x.GetListAsync(filterDao, _defaultCancellationToken), Times.Once);
            _mapperMock.Verify(x => x.Map<CostingLaborFilterDao>(filter), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<CostingLabor>>(It.IsAny<IEnumerable<CostingLaborDao>>()), Times.Once);
        }
    }
}