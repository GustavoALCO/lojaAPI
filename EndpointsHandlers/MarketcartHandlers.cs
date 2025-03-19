using loja_api.Mapper.MarketCart;
using loja_api.Mapper.User;
using loja_api.Services;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace loja_api.EndpointsHandlers;

public static class MarketcartHandlers
{
    public static async Task<Results<Ok<string>, BadRequest>> CreateMarketCart(MarketCartService marketCartService, [FromBody] MarketCartDTO marketCartDTO)
    {
        try
        {
            var paymant = await marketCartService.CreatePaymant(marketCartDTO);

            if (paymant.IsNullOrEmpty())
                return TypedResults.BadRequest();

            return TypedResults.Ok(paymant);
        }
        catch 
        {
            return TypedResults.BadRequest();
        }
    }

    public static async Task<Results<Ok<Preference>, BadRequest>> CreateMarketCartTeste(MarketCartService marketCartService)
    {
        
        try
        {

            var paymant = await marketCartService.CreatePaymantTest();

            if (paymant == null)
                return TypedResults.BadRequest();

            return TypedResults.Ok(paymant);
        }
        catch
        {
            return TypedResults.BadRequest();
        }
    }

    public static async Task<Results<Ok, NotFound>> WebHook(MarketCartService marketCartService, dynamic data)
    {
        try
        {
            var webhook = await marketCartService.RecibeWebHook(data);

            if (webhook == null)
                return TypedResults.NotFound();

            return TypedResults.Ok();
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }
}
