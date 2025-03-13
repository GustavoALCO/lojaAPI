using loja_api.Entities.auxiliar;
using System.ComponentModel.DataAnnotations;

namespace loja_api.Entities;

public class MarketCart
{
    [Key]
    public Guid MarketCartId { get; set; }

    public Guid UserId { get; set; }

    public Guid CupomId { get; set; }

    public List<ProductsMarketCart> ProductsMarketCart { get; set; }

    public double Price { get; set; }

    public Attdata AttDate { get; set; }

    public User User { get; set; }

    public Cupom Cupom { get; set; }
}
