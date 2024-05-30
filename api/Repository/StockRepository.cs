using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<StockModel>> GetAllAsync(QueryObject query)
        {
            var records = _context.Stocks.AsQueryable();


            records = records.Where(c => c.CompanyName.Contains(query.CompanyName!));

            records = records.Where(c => c.Symbol.Contains(query.Symbol!));
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    records = query.IsDescending ? records.OrderByDescending(s => s.Symbol) : records.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PagesNumber - 1) * query.PagesSize;
            return await records.Skip(skipNumber).Take(query.PagesSize).ToListAsync();
        }

        public async Task<StockModel?> GetByIdAsync(Guid id)
        {
            var record = await _context.Stocks.SingleOrDefaultAsync(x => x.Id == id);

            return record;
        }
        public async Task<bool> CreateAsync(StockModel stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var record = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (record is null) return false;

            _context.Stocks.Remove(record);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateStockRequestDto stockDto)
        {
            var record = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (record is null) return false;

            record.Industry = stockDto.Industry;
            record.CompanyName = stockDto.CompanyName;
            record.LastDiv = stockDto.LastDiv;
            record.MarketCap = stockDto.MarketCap;
            record.Purchase = stockDto.Purchase;
            record.Symbol = stockDto.Symbol;
            _context.Stocks.Update(record);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> StockExists(Guid id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id);
        }
    }
}