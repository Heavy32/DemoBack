using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Storage
{
    public interface IStorageItemImportService
    {
        /// <summary>
        /// Imports Storage items from an Excel file
        /// </summary>
        /// <param name="fileStream">The Excel file stream</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A collection of imported Storage items</returns>
        Task<IEnumerable<StorageItem>> ImportFromExcelAsync(Stream fileStream, CancellationToken ct = default);
    }
} 