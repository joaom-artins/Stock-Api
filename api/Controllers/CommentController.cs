using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepository,IStockRepository stockRepository)
        {
            _commentRepo=commentRepository;
            _stockRepo=stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments= await _commentRepo.GetAllAsync();
            var commentDto=comments.Select(s=>s.ToCommentDto());

            return Ok(commentDto); 
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment=await _commentRepo.GetByIdAsync(id);
            if(comment is null) return NotFound();
            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Post([FromRoute] int stockId,[FromBody] CreatedCommentDto commentDto)
        {
            if(!await _stockRepo.StockExists(stockId)) return BadRequest("Stock does not exist");
            var commentModel= commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById),new {id=commentModel.Id},commentModel.ToCommentDto());
        }
    }
}