namespace FlowCycle.Domain.Costing
{
    public interface ICostingImportService
    {
        /// <summary>
        /// Imports costing data from an Excel file
        /// </summary>
        /// <param name="fileStream">The Excel file stream</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The imported costing model</returns>
        Task<List<CostingModel>> ImportFromExcelAsync(Stream fileStream, CancellationToken ct = default);
    }
}