using loja_api.Mapper.Product;
using loja_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace loja_api.EndpointsHandlers;

public static class ProductsHanders
{
    public static async Task<Results<Ok<IEnumerable<ProductsDTO>>, NotFound<string>>> GetProducts(ProductsService productsService,
                                                                                          string? nome)
    {
        try
        {
            var products = await productsService.GetProducts(nome);

            if (products.IsNullOrEmpty())
                return TypedResults.NotFound("Não foi possivel Encontrar os produtos Visualize o Console para mais informaçoes");

            return TypedResults.Ok(products);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Results<Ok<ProductsDTO>, NotFound<string>>> GetProductsID(ProductsService productsService,
                                                                                          Guid ID)
    {
        try
        {
            var products = await productsService.GetProductsID(ID);

            if (products == null)
                return TypedResults.NotFound("Não foi possivel Encontrar os produtos Visualize o Console para mais informaçoes");

            return TypedResults.Ok(products);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Results<Ok<ProductsDTO>, NotFound<string>>> PostProducts(ProductsService productsService,
                                                                                      ProductsCreateDTO createDTO)
    {
        try
        {
            var products = await productsService.CreateProductsDTO(createDTO);

            if (products == null)
                return TypedResults.NotFound("Não foi possivel Encontrar os produtos Visualize o Console para mais informaçoes");

            return TypedResults.Ok(products);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Results<Ok<ProductsDTO>, NotFound<string>>> PutProducts(ProductsService productsService,
                                                                                      ProductsUpdateDTO UpdateDTO)
    {
        try
        {
            var products = await productsService.UpdateProducts(UpdateDTO);

            if (products == null)
                return TypedResults.NotFound("Não foi possivel Encontrar os produtos Visualize o Console para mais informaçoes");

            return TypedResults.Ok(products);
        }
        catch (Exception ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    public static async Task<Results<Ok<string>, BadRequest<string>>> DeleteProducts(ProductsService productsService,
                                                                                     Guid id)
    {
        try
        {
            var products = await productsService.DeleteProducts(id);

            if (products == false)
                return TypedResults.BadRequest("Não foi possivel Encontrar os produtos Visualize o Console para mais informaçoes");

            return TypedResults.Ok("Produto Excluido com Sucesso");
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}
