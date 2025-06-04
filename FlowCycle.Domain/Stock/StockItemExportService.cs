using ClosedXML.Excel;
using FlowCycle.Domain.Stock;
using FlowCycle.Persistance;
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlowCycle.Domain.Stock
{
    public class StockItemExportService : IStockItemExportService
    {
        private readonly AppDbContext _context;

        public StockItemExportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MemoryStream> ExportToExcelAsync(CancellationToken ct = default)
        {
            // Get all stock items with their related entities
            var stockItems = await _context.Stocks
                .Include(s => s.Supplier)
                .Include(s => s.Project)
                .Include(s => s.Category)
                .ToListAsync(ct);

            // Create a new Excel workbook
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Stock Items");

            // Add headers
            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Code";
            worksheet.Cell(1, 3).Value = "Category";
            worksheet.Cell(1, 4).Value = "Measure";
            worksheet.Cell(1, 5).Value = "Amount";
            worksheet.Cell(1, 6).Value = "Single Price";
            worksheet.Cell(1, 7).Value = "VAT";
            worksheet.Cell(1, 8).Value = "Total Price";
            worksheet.Cell(1, 9).Value = "Receipt Date";
            worksheet.Cell(1, 10).Value = "Supplier";
            worksheet.Cell(1, 11).Value = "Project";
            worksheet.Cell(1, 12).Value = "Is Archived";

            // Style the header row
            var headerRange = worksheet.Range(1, 1, 1, 12);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Add data
            int row = 2;
            foreach (var item in stockItems)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Code;
                worksheet.Cell(row, 3).Value = item.Category?.Name;
                worksheet.Cell(row, 4).Value = item.Measure;
                worksheet.Cell(row, 5).Value = item.Amount;
                worksheet.Cell(row, 6).Value = item.SinglePrice;
                worksheet.Cell(row, 7).Value = item.VAT;
                worksheet.Cell(row, 8).Value = item.TotalPrice;
                worksheet.Cell(row, 9).Value = item.ReceiptDate;
                worksheet.Cell(row, 10).Value = item.Supplier?.Name;
                worksheet.Cell(row, 11).Value = item.Project?.Name;
                worksheet.Cell(row, 12).Value = item.IsArchived;
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Create memory stream and save workbook
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }
    }
}