using AutoMapper;
using loja_api.Entities;
using loja_api.Entities.auxiliar;
using loja_api.Mapper.MarketCart;
using loja_api.Mapper.Product;

namespace loja_api.Profiles;

public class MarketCartProfiles : Profile
{
    public MarketCartProfiles()
    {
        CreateMap<MarketCart, MarketCartDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductsMarketCart
            .Select(mp => new ProductsMarketCart
            {
                IdProducts = mp.Products.IdProducts,
                Price = mp.Products.Price,
                Quantity = mp.Quantity 
            }).ToList()));
    }
}
