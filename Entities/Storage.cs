using System.ComponentModel.DataAnnotations;

namespace loja_api.Entities;

public class Storage
{
    [Key]
    public Guid IdStorage { get; set; }

    public Guid IdProducts { get; set; }

    public int Quantity { get; set; }

    public double PriceBuy { get; set; }

    public Products Products { get; set; }

    public Auditable Auditable { get; set; }
}
