using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly AppDbContext _context;
        public StockController(AppDbContext context)
        {
            _context=context; 
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks=_context.Stocks.ToList().Select(s=>s.ToStockDto());
            if(stocks is null) return BadRequest();

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock=_context.Stocks.Find(id);
            if(stock is null) return NotFound();

            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateStockRequestDto stockDto)
        {
           var stockModel=stockDto.ToStockFromCreateDTO();
           _context.Stocks.Add(stockModel);
           _context.SaveChanges();
           return CreatedAtAction(nameof(GetById),new {id=stockModel.Id},stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel= _context.Stocks.FirstOrDefault(x=>x.Id==id);

            if(stockModel is null) return NotFound();

            stockModel.Symbol=updateDto.Symbol;
            stockModel.CompanyName=updateDto.CompanyName;
            stockModel.Industry=updateDto.Industry;
            stockModel.LastDiv=updateDto.LastDiv;
            stockModel.MarketCap=updateDto.MarketCap;
            stockModel.Purchase=updateDto.Purchase;
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }
    }
}