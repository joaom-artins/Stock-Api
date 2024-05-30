using api.Dtos.Comment;
using api.Interfaces;
using api.Model;
using api.Services.Interfaces;

namespace api.Services;

public class CommentService(
    ICommentRepository _commentRepository,
    IStockRepository _stockRepository
) : ICommentService
{
    public async Task<bool> CreateAsync(Guid stockId, CreatedCommentDto request)
    {
        var stock = await _stockRepository.GetByIdAsync(stockId);
        if (stock is null)
        {
            return false;
        }

        var record = new CommentModel
        {
            Title = request.Title,
            Content = request.Content,
            StockId = stockId,
        };
        await _commentRepository.AddAsync(record);

        return true;
    }
}
