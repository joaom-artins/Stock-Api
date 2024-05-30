namespace api.Model;

public class CommentModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public Guid? StockId { get; set; }
    public StockModel Stock { get; set; } = default!;
}
