﻿using AutoMapper;
using loja_api.Entities.auxiliar;
using loja_api.Entities;
using loja_api.Mapper.Cupom;
using loja_api.Mapper.Emploree;

namespace loja_api.Profiles;

public class CupomProfille : Profile
{
    public CupomProfille() 
    {
        CreateMap<Cupom, CupomDTO>().ReverseMap();

        CreateMap<Cupom, CupomCreateDTO>()
                    // Mapeia as propriedades de "Auditable" diretamente para "StorageDTO" 
                    .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Auditable.CreateDate))
                    .ForMember(dest => dest.CreatebyId, opt => opt.MapFrom(src => src.Auditable.CreatebyId))
                    .ReverseMap()
                    //recria um objeto "Auditable"
                    .ForMember(dest => dest.Auditable, opt => opt.MapFrom(src => new Auditable
                    {
                        CreateDate = src.CreateDate,
                        CreatebyId = src.CreatebyId,
                    }));

        CreateMap<Cupom, CupomUpdateDTO>()
                    // Mapeia as propriedades de "Auditable" diretamente para "StorageDTO" 
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.Auditable.UpdateDate))
                    .ForMember(dest => dest.UpdatebyId, opt => opt.MapFrom(src => src.Auditable.UpdatebyId))
                    .ReverseMap()
                    //recria um objeto "Auditable"
                    .ForMember(dest => dest.Auditable, opt => opt.MapFrom(src => new Auditable
                    {
                        UpdateDate = src.UpdateDate,
                        UpdatebyId = src.UpdatebyId,
                    }))
                    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
