using Invention.Emums;

namespace Invention.Models.Commons;

public class Filter
{
    public long? SupplierId { get; set; }
    public long? MarketId { get; set; }
    public long? ProductId { get; set; }
    public DateFilter? Date { get; set; }
}
