using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace FlowCycle.Persistance.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly AppDbContext _context;

        public StockItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StockItemDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Stocks
                .Include(s => s.Category)
                .Include(s => s.Project)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<IEnumerable<StockItemDao>> GetListAsync(StockItemFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _context.Stocks
                .Include(s => s.Category)
                .Include(s => s.Project)
                .Include(s => s.Supplier)
                .AsQueryable();

            if (filter != null)
            {
                // Apply filters
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    query = query.Where(s => s.Name.Contains(filter.Name));
                }

                if (!string.IsNullOrWhiteSpace(filter.Supplier))
                {
                    query = query.Where(s => s.Supplier.Name.Contains(filter.Supplier));
                }

                if (!string.IsNullOrWhiteSpace(filter.Code))
                {
                    query = query.Where(s => s.Code.Contains(filter.Code));
                }

                // Apply sorting
                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    var sortColumn = filter.SortColumn.Trim();
                    var direction = filter.SortDescending ? "descending" : "ascending";

                    // Map frontend column names to actual property names
                    sortColumn = MapSortColumn(sortColumn);

                    try
                    {
                        // Use Dynamic.Core's OrderBy method with proper string formatting
                        var orderByExpression = $"{sortColumn} {direction}";
                        query = query.OrderBy(orderByExpression);
                    }
                    catch (Exception ex)
                    {
                        // If dynamic sorting fails, fall back to default sorting
                        query = query.OrderBy(s => s.Name);
                    }
                }
                else
                {
                    // Default sorting if no sort column specified
                    query = query.OrderBy(s => s.Name);
                }
            }
            else
            {
                // Default sorting if no filter provided
                query = query.OrderBy(s => s.Name);
            }

            return await query.ToListAsync(ct);
        }

        private string MapSortColumn(string column)
        {
            // Map frontend column names to actual property names
            return column.ToLower() switch
            {
                "name" => "Name",
                "code" => "Code",
                "supplier" => "Supplier.Name",
                "category" => "Category.Name",
                "project" => "Project.Name",
                "price" => "Price",
                "quantity" => "Quantity",
                "receiptdate" => "ReceiptDate",
                "isarchived" => "IsArchived",
                _ => column // Return original if no mapping found
            };
        }

        public async Task<StockItemDao> CreateAsync(StockItemDao stockItem, CancellationToken ct)
        {
            _context.Stocks.Add(stockItem);
            return stockItem;
        }

        public async Task<StockItemDao> UpdateAsync(StockItemDao stockItem, CancellationToken ct)
        {
            var existingItem = await _context.Stocks.FindAsync(new object[] { stockItem.Id }, ct);
            if (existingItem == null)
            {
                return null;
            }

            _context.Entry(existingItem).CurrentValues.SetValues(stockItem);
            return existingItem;
        }

        public async Task DeleteAsync(StockItemDao stockItem, CancellationToken ct)
        {
            _context.Stocks.Remove(stockItem);
        }
    }
}