using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model;

public class StockModel
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    [Column(TypeName = "Decimal(18,2)")]
    public decimal Purchase { get; set; }
    [Column(TypeName = "Decimal(18,2)")]
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }

}
