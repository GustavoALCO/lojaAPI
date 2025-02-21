namespace loja_api.Entities.auxiliar;

public class ProductsMarketCart
{
    public Guid MarketCartId { get; set; }

    public Guid IdProducts { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public Products Products { get; set; }

    public MarketCart MarketCart { get; set; }
}
