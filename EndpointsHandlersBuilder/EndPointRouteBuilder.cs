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

    public static void RegisterUserEndPoints(this IEndpointRouteBuilder EndPointRoute)
    {
        var userEndPoints = EndPointRoute.MapGroup("/User");

        userEndPoints.MapGet("", Userhandler.GetUsers)
            .WithSummary("Busca Todos os Usuarios Se deixar nulo ou todos pelo email do Produto");

        userEndPoints.MapGet("{Id}", Userhandler.GetUsers)
            .WithSummary("Busca usuarios pelo ID");

        userEndPoints.MapPost("", Userhandler.CreateUser)
            .WithSummary("Usado Para Criar um nova Storage");

        userEndPoints.MapPut("", Userhandler.UpdateUser).
            WithSummary("Usado para Alterar as propriedades da Storage");

        userEndPoints.MapDelete("{Id}", Userhandler.DeleteUser)
            .WithSummary("Usado para excluir a Storage do banco de dados");
    }

    public static void RegisterEmployeeEndPoints(this IEndpointRouteBuilder EndPointRoute)
    {
        var userEndPoints = EndPointRoute.MapGroup("/Employee");

        userEndPoints.MapGet("", EmployeeHandlers.GetEmployees)
            .WithSummary("Busca Todos os Usuarios Se deixar nulo ou todos pelo email do Produto");

        userEndPoints.MapGet("{Id}", EmployeeHandlers.GetEmployeeId)
            .WithSummary("Busca usuarios pelo ID");

        userEndPoints.MapPost("", EmployeeHandlers.CreateEmployee)
            .WithSummary("Usado Para Criar um nova Storage");

        userEndPoints.MapPut("", EmployeeHandlers.UpdateEmployee).
            WithSummary("Usado para Alterar as propriedades da Storage");

        userEndPoints.MapDelete("{Id}", EmployeeHandlers.DeleteEmployee)
            .WithSummary("Usado para excluir a Storage do banco de dados");
    }

    public static void RegisterLoginEndPoints(this IEndpointRouteBuilder EndPointRoute)
    {
        var loginEndPoints = EndPointRoute.MapGroup("/Login");

        loginEndPoints.MapPost("/User", Userhandler.Login)
            .WithSummary("Usado Para Criar um nova Storage");

        loginEndPoints.MapPost("/Employee", EmployeeHandlers.Login)
            .WithSummary("Login Apenas para Usuarios");
    }

    public static void RegisterMercadoPagoEndPoints(this IEndpointRouteBuilder EndPointRoute)
    {
        var route = EndPointRoute.MapGroup("/paymant");

        route.MapPost("", MarketcartHandlers.CreatePaymentFromToken);
    }

}
