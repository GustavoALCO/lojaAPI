using loja_api.Entities.auxiliar;
using loja_api.Mapper.MarketCart.ProductMarketCart;

namespace loja_api.Mapper.MarketCart;
public class MarketCartDTO
{
    public Guid MarketCartId { get; set; }

    public Guid UserId { get; set; }

    public Guid CupomId { get; set; }

    public List<ProductMarketCartDTO> ProductsMarket { get; set; }

    public double Price { get; set; }

    public Attdata AttDate { get; set; }
}

