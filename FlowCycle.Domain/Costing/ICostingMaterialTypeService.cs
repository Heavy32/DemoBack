namespace FlowCycle.Domain.Costing
{
    public interface ICostingMaterialTypeService
    {
        Task<CostingMaterialType?> GetByIdAsync(int id, CancellationToken ct);
        Task<CostingMaterialType?> GetByNameAsync(string name, CancellationToken ct);
        Task<IEnumerable<CostingMaterialType>> GetAllAsync(CancellationToken ct);
        Task<CostingMaterialType> CreateAsync(CostingMaterialType materialType, CancellationToken ct);
        Task<CostingMaterialType> UpdateAsync(CostingMaterialType materialType, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}