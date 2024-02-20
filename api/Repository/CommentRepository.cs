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
    public class CommentRepository:ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context=context;
        }

       

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id){
            var comment=await _context.Comments.FindAsync(id);
            if(comment is null) return null;
            return comment;
        }
    
         public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var commentModel=await _context.Comments.FindAsync(id);
            if(commentModel is null) return null;
            commentModel.Title=comment.Title;
            commentModel.Content=comment.Content;
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> RemoveAsync(int id)
        {
            var commentModel=await _context.Comments.FindAsync(id);
            if(commentModel is null) return null;
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
    }
}