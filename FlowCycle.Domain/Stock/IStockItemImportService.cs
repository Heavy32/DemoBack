using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Stock
{
    public interface IStockItemImportService
    {
        /// <summary>
        /// Imports stock items from an Excel file
        /// </summary>
        /// <param name="fileStream">The Excel file stream</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A collection of imported stock items</returns>
        Task<IEnumerable<StockItem>> ImportFromExcelAsync(Stream fileStream, CancellationToken ct = default);
    }
} 