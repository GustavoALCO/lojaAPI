
namespace loja_api.EndpointsHandlers;

public static class EndPointRouteBuilder
{

    public static void RegisterCupomEndPoint(this IEndpointRouteBuilder EndpointRoute) 
    {
        var CupomEndPoint = EndpointRoute.MapGroup("/Cupom");

        CupomEndPoint.MapGet("", Cupomhandler.GetCupons)
            .WithSummary("Usado para Busca, Se não passar nenhum Valor ele vai fazer uma busca Completa");

        CupomEndPoint.MapPost("", Cupomhandler.CreateCupom)
            .WithSummary("Usado para Criar Novos Cupons Com Obrigatoriedade de passar. Quantity, ExpirationDate, Discount, Name");

        CupomEndPoint.MapPut("", Cupomhandler.UpdateCupom)
            .WithSummary("Usado para Atualizar Cupons existentes, Sendo Necessario passar o Id dentro do Body");

        CupomEndPoint.MapDelete("/{Id}", Cupomhandler.DeleteCupom);
    }
}
