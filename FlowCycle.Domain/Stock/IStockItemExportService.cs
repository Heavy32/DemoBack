namespace FlowCycle.Domain.Stock
{
    public interface IStockItemExportService
    {
        /// <summary>
        /// Exports stock items to an Excel file
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Memory stream containing the Excel file</returns>
        Task<MemoryStream> ExportToExcelAsync(CancellationToken ct = default);
    }
}