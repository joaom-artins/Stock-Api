using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo=commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments= await _commentRepo.GetAllAsync();
            var commentDto=comments.Select(s=>s.ToCommentDto());

            return Ok(commentDto); 
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment=await _commentRepo.GetByIdAsync(id);
            if(comment is null) return NotFound();
            return Ok(comment.ToCommentDto());
        }
    }
}