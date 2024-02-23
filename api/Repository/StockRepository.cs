using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context=context;
        }

       

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks=  _context.Stocks.Include(c=>c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks=stocks.Where(c=>c.CompanyName.Contains(query.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks=stocks.Where(c=>c.Symbol.Contains(query.Symbol));
            }
            return await stocks.ToListAsync();
        }   

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(x=>x.Id==id);
        }
         public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> RemoveAsync(int id)
        {
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel is null) return null;
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id,UpdateStockRequestDto stockDto)
        {
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel is null) return null;

            stockModel.Industry=stockDto.Industry;
            stockModel.CompanyName=stockDto.CompanyName;
            stockModel.LastDiv=stockDto.LastDiv;
            stockModel.MarketCap=stockDto.MarketCap;
            stockModel.Purchase=stockDto.Purchase;
            stockModel.Symbol=stockDto.Symbol;
            
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(x=>x.Id==id);
        }
    }
}