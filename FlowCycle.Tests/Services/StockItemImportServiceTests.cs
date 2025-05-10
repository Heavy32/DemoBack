using AutoMapper;
using FlowCycle.Domain.Stock;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;

namespace FlowCycle.Tests.Services
{
    [TestFixture]
    public class StockItemImportServiceTests
    {
        private Mock<IStockItemRepository> _stockItemRepositoryMock;
        private Mock<ISupplierRepository> _supplierRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<ILogger<StockItemImportService>> _loggerMock;
        private IMapper _mapper;
        private StockItemImportService _service;

        [SetUp]
        public void Setup()
        {
            _stockItemRepositoryMock = new Mock<IStockItemRepository>();
            _supplierRepositoryMock = new Mock<ISupplierRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _loggerMock = new Mock<ILogger<StockItemImportService>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SupplierDao, Supplier>();
                cfg.CreateMap<CategoryDao, Category>();
                cfg.CreateMap<ProjectDao, Project>();
            });
            _mapper = mapperConfig.CreateMapper();

            _service = new StockItemImportService(
                _stockItemRepositoryMock.Object,
                _supplierRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _projectRepositoryMock.Object,
                _mapper
            );
        }

        [Test]
        public async Task ImportFromExcelAsync_WithRealFile_ImportsAndReturnsItems()
        {
            // Arrange
            var testDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var projectDirectory = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", ".."));
            var filePath = Path.Combine(projectDirectory, "FlowCycle.Tests", "TestData", "Запасы - Тестовые данные и примеры.xlsx");
            
            Console.WriteLine($"Looking for file at: {filePath}");
            
            if (!File.Exists(filePath))
            {
                Assert.Fail($"Test data file not found at: {filePath}");
            }

            using var fileStream = File.OpenRead(filePath);

            // Setup repository mocks to return test data
            _supplierRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string name, CancellationToken ct) => new SupplierDao { Id = 1, Name = name });

            _categoryRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string name, CancellationToken ct) => new CategoryDao { Id = 1, Name = name });

            _projectRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string name, CancellationToken ct) => new ProjectDao { Id = 1, Name = name });

            // Act
            var result = await _service.ImportFromExcelAsync(fileStream);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            
            // Verify that the service attempted to look up entities
            _supplierRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            _categoryRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            _projectRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Log the number of items imported
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Imported")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.AtLeastOnce);
        }
    }
} 