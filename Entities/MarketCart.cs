namespace loja_api.Entities;

public class MarketCart
{
    public int Id { get; set; }

    public Guid IdUser { get; set; }

    public double Price { get; set; }

    public ICollection<Products> Products { get; set; }

    public Cupom Cupom { get; set; }

    public ICollection<Status> Status { get; set; }

    
}
