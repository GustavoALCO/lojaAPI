
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

    public static void RegisterStorageEndPoints(this IEndpointRouteBuilder EndpointRoute)
    {
        var StorageEndPoints = EndpointRoute.MapGroup("/Storage");

        StorageEndPoints.MapGet("", StorageHandlers.GetStorage)
            .WithSummary("Busca Todos os Storages Se deixar nulo ou todos pelo ID do Produto");

        StorageEndPoints.MapGet("{Id}", StorageHandlers.GetStorageId)
            .WithSummary("Busca por um Storage especifico pelo ID");

        StorageEndPoints.MapPost("", StorageHandlers.CreateStorage)
            .WithSummary("Usado Para Criar um nova Storage");

        StorageEndPoints.MapPut("", StorageHandlers.UpdateStorage).
            WithSummary("Usado para Alterar as propriedades da Storage");

        StorageEndPoints.MapPut("/{Isvalid}/{Id}", StorageHandlers.UpdateIsValid)
            .WithSummary("Usado apenas para mudar o status do IsValid sendo passado um bool");

        StorageEndPoints.MapDelete("{Id}", StorageHandlers.DeleteStorage)
            .WithSummary("Usado para excluir a Storage do banco de dados");
    }
}
