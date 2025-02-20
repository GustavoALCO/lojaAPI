namespace loja_api.Entities;

public class Storage
{
    public Guid IdStorage { get; set; }

    public int Quantity { get; set; }

    public double PriceBuy { get; set; }

    Auditable Auditable { get; set; }
}
