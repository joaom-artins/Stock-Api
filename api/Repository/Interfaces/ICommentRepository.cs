using api.Model;

namespace api.Interfaces;

public interface ICommentRepository
{
    Task<List<CommentModel>> GetAllAsync();

    Task<CommentModel?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(CommentModel comment);
    Task<bool> UpdateAsync(Guid id, CommentModel comment);
    Task<bool> RemoveAsync(Guid id);

}
