using AutoMapper;
using loja_api.Entities.auxiliar;
using loja_api.Entities;
using loja_api.Mapper.Product;

namespace loja_api.Profiles;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Products, ProductsDTO>().ReverseMap();

        CreateMap<Products, ProductsCreateDTO>()
                    // Mapeia as propriedades de "Auditable" diretamente para "ProductsDTO" 
                    .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Auditable.CreateDate))
                    .ForMember(dest => dest.CreatebyId, opt => opt.MapFrom(src => src.Auditable.CreatebyId))
                    .ReverseMap()
                    //recria um objeto "Auditable"
                    .ForMember(dest => dest.Auditable, opt => opt.MapFrom(src => new Auditable
                    {
                        CreateDate = src.CreateDate,
                        CreatebyId = src.CreatebyId,
                    }));

        CreateMap<Products, ProductsUpdateDTO>()
                    // Mapeia as propriedades de "Auditable" diretamente para "ProductsDTO" 
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Auditable.UpdateDate))
                    .ForMember(dest => dest.UpdatebyId, opt => opt.MapFrom(src => src.Auditable.UpdatebyId))
                    .ReverseMap()
                    //recria um objeto "Auditable"
                    .ForMember(dest => dest.Auditable, opt => opt.MapFrom(src => new Auditable
                    {
                        UpdateDate = src.UpdateDate,
                        UpdatebyId = src.UpdatebyId,
                    })).ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 


    }
}
