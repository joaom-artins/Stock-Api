using api.Dtos.Stock;
using api.Helpers;
using api.Model;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<IEnumerable<StockModel>> GetAllAsync(QueryObject query);
    Task<StockModel?> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(StockModel stockModel);
    Task<bool> RemoveAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateStockRequestDto stockDto);
    Task<bool> StockExists(Guid id);
}
