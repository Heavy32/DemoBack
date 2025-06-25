using AutoMapper;
using ClosedXML.Excel;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;
using FlowCycle.Persistance.UnitOfWork;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FlowCycle.Tests.Services
{
    [TestFixture]
    public class CostingImportServiceTests
    {
        private Mock<ICostingRepository> _costingRepositoryMock;
        private Mock<ICostingMaterialRepository> _costingMaterialRepositoryMock;
        private Mock<ICostingLaborRepository> _costingLaborRepositoryMock;
        private Mock<ICostingOverheadRepository> _costingOverheadRepositoryMock;
        private Mock<ICostingMaterialTypeRepository> _costingMaterialTypeRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CostingImportService _costingImportService;
        private CancellationToken _defaultCancellationToken;

        [SetUp]
        public void Setup()
        {
            _costingRepositoryMock = new Mock<ICostingRepository>();
            _costingMaterialRepositoryMock = new Mock<ICostingMaterialRepository>();
            _costingLaborRepositoryMock = new Mock<ICostingLaborRepository>();
            _costingOverheadRepositoryMock = new Mock<ICostingOverheadRepository>();
            _costingMaterialTypeRepositoryMock = new Mock<ICostingMaterialTypeRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _costingImportService = new CostingImportService(
                _costingRepositoryMock.Object,
                _costingMaterialRepositoryMock.Object,
                _costingLaborRepositoryMock.Object,
                _costingOverheadRepositoryMock.Object,
                _mapperMock.Object,
                _costingMaterialTypeRepositoryMock.Object,
                _unitOfWorkMock.Object);
            _defaultCancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task ImportFromExcelAsync_WithRealFile_ShouldImportAllSections()
        {
            // Arrange
            var testDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var projectDirectory = Path.GetFullPath(Path.Combine(testDirectory, "..", "..", "..", ".."));
            var filePath = Path.Combine(projectDirectory, "FlowCycle.Tests", "TestData", "Себестоимость - тестовые данные.xlsx");

            TestContext.WriteLine($"Looking for file at: {filePath}");

            if (!File.Exists(filePath))
            {
                Assert.Fail($"Test data file not found at: {filePath}");
            }

            // Mock project with known name from file
            var project = new ProjectDao
            {
                Id = 1,
                Name = "КМПЗ-4521"
            };

            // Mock costing type with known name from file
            var costingType = new CostingTypeDao
            {
                Id = 1,
                Name = "Фактический"
            };


            // Mock overhead type with known name from file
            var overheadType1 = new OverheadTypeDao
            {
                Id = 1,
                Name = "Прямой"
            };

            var overheadType2 = new OverheadTypeDao
            {
                Id = 2,
                Name = "Косвенный"
            };

            var expectedCosting = new CostingDao
            {
                Id = 1,
                ProjectId = project.Id,
                CostingTypeId = costingType.Id,
                ProductName = "Кран шаровой СПМ-КШ300.80.ФО.ПР.Н.ХЛ1, металл-металл",
                Uom = "Компл.",
                Quantity = 1,
                UnitCost = 1000,
                TotalCost = 1000,
                Project = project,
                CostingType = costingType
            };

            // Setup repository mocks with known names from file
            _costingRepositoryMock.Setup(x => x.GetProjectByNameAsync("Проект 1", It.IsAny<CancellationToken>()))
                .ReturnsAsync(project);

            _costingRepositoryMock.Setup(x => x.GetCostingTypeByNameAsync("Материал", It.IsAny<CancellationToken>()))
                .ReturnsAsync(costingType);

            _costingRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CostingDao>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCosting);

            _costingOverheadRepositoryMock.Setup(x => x.GetByNameAsync("Прямой", It.IsAny<CancellationToken>()))
                .ReturnsAsync(overheadType1);

            _costingOverheadRepositoryMock.Setup(x => x.GetByNameAsync("Косвенный", It.IsAny<CancellationToken>()))
                .ReturnsAsync(overheadType2);

            var expectedCostingModel = new CostingModel
            {
                Id = expectedCosting.Id,
                ProjectId = expectedCosting.ProjectId,
                CostingTypeId = expectedCosting.CostingTypeId,
                ProductName = expectedCosting.ProductName,
                Uom = expectedCosting.Uom,
                Quantity = expectedCosting.Quantity,
                UnitCost = expectedCosting.UnitCost,
                TotalCost = expectedCosting.TotalCost
            };

            _mapperMock.Setup(x => x.Map<CostingModel>(It.IsAny<CostingDao>()))
                .Returns(expectedCostingModel);

            // Act
            using var fileStream = File.OpenRead(filePath);
            var result = await _costingImportService.ImportFromExcelAsync(fileStream);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            var costing = result[0];
            Assert.That(costing.Id, Is.EqualTo(expectedCostingModel.Id));
            Assert.That(costing.ProjectId, Is.EqualTo(expectedCostingModel.ProjectId));
            Assert.That(costing.CostingTypeId, Is.EqualTo(expectedCostingModel.CostingTypeId));
            Assert.That(costing.ProductName, Is.EqualTo(expectedCostingModel.ProductName));
            Assert.That(costing.Uom, Is.EqualTo(expectedCostingModel.Uom));
            Assert.That(costing.Quantity, Is.EqualTo(expectedCostingModel.Quantity));
            Assert.That(costing.UnitCost, Is.EqualTo(expectedCostingModel.UnitCost));
            Assert.That(costing.TotalCost, Is.EqualTo(expectedCostingModel.TotalCost));

            // Verify that all sections were processed
            _costingRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingDao>(), It.IsAny<CancellationToken>()), Times.Once);
            _costingMaterialRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingMaterialDao>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            _costingLaborRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingLaborDao>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            _costingOverheadRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CostingOverheadDao>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}