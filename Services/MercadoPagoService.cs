
using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Mapper.Emploree;
using loja_api.Mapper.MarketCart;
using loja_api.Mapper.User;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;

namespace loja_api.Services;

public class MercadoPagoService
{

    public async Task<Preference> CreatePaymant(MarketCartDTO marketCartDTO, UserDTO user)
    {
        var request = new PreferenceRequest
        {
            Items = new List<PreferenceItemRequest>
        {

        new PreferenceItemRequest
        {
            Id = Guid.NewGuid().ToString(),
            Title = "Finalizando Compra No Loja-Peças",
            Quantity = marketCartDTO.ProductsMarket.Count,
            CurrencyId = "BRL",
            UnitPrice = ((decimal)marketCartDTO.Price),
        },

    },
            Payer = new PreferencePayerRequest
            {
                DateCreated= DateTime.Now,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
            },
        };

        // Cria a preferência usando o client
        var client = new PreferenceClient();
        Preference preference = await client.CreateAsync(request);

        return preference;
    }

    public async Task<Preference> CreatePaymantTest(MarketCartDTO marketCartDTO, UserDTO user)
    {
        var request = new PreferenceRequest
        {
            Items = new List<PreferenceItemRequest>
        {

        new PreferenceItemRequest
        {
            Title = "Finalizando Compra No Loja-Peças",
            Quantity = 1,
            CurrencyId = "BRL",
            UnitPrice = ((decimal)marketCartDTO.Price),
        },

    },
            Payer = new PreferencePayerRequest
            {
                DateCreated = DateTime.Now,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
            },
        };

        // Cria a preferência usando o client
        var client = new PreferenceClient();
        Preference preference = await client.CreateAsync(request);

        return preference;
    }


}
    
