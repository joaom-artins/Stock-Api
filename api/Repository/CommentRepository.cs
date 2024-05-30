using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository(AppDbContext context) : ICommentRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<CommentModel>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<CommentModel?> GetByIdAsync(Guid id)
        {
            var record = await _context.Comments.SingleOrDefaultAsync(x => x.Id == id);
            if (record is null) return null;

            return record;
        }

        public async Task<bool> AddAsync(CommentModel comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Guid id, CommentModel comment)
        {
            var record = await _context.Comments.SingleOrDefaultAsync(x => x.Id == id);
            if (record is null) return false;

            record.Title = comment.Title;
            record.Content = comment.Content;
            _context.Update(record);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var record = await _context.Comments.SingleOrDefaultAsync(x => x.Id == id);
            if (record is null) return false;

            _context.Comments.Remove(record);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}