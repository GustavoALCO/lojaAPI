using loja_api.Mapper.Storage;
using loja_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace loja_api.EndpointsHandlers;

public static class StorageHandlers
{

    public static async Task<Results<BadRequest<string>,Ok<IEnumerable<StorageDTO>>>> GetStorage
                                                                            (StorageServices storageServices,
                                                                             Guid? ID)
    {
        try
        {
            var storage = await storageServices.GetStorage(ID);

            if (storage == null)
                return TypedResults.BadRequest("O Banco de Dados da Storage esta Vazio");

            return TypedResults.Ok(storage);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<StorageDTO>, BadRequest<string>>> GetStorageId
                                                        (StorageServices storageServices,
                                                        Guid id 
                                                        )
    {
        try
        {
            var storage = await storageServices.GetStorageID(id);

            if (storage == null)
                return TypedResults.BadRequest("Visualize o Console para mais Informações");

            return TypedResults.Ok(storage);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }

    }

    public static async Task<Results<Ok<StorageDTO>,BadRequest<string>>> CreateStorage
                                                            (StorageServices storageServices,
                                                             [FromBody]
                                                             StorageCreateDTO createDTO)
    {
        try
        {
            var storage = await storageServices.CreateStorage(createDTO);

            if (storage == null)
                return TypedResults.BadRequest("Visualize o Console para mais Informações");

            return TypedResults.Ok(storage);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }

    public static async Task<Results<Ok<StorageDTO>, BadRequest<string>>> UpdateStorage(
                                                                       StorageServices storageServices,
                                                                       [FromBody]
                                                                       StorageUpdateDTO UpdateDTO)
    {
        try
        {
           var storage = await storageServices.UpdateStorage(UpdateDTO);

            if(storage == null)
                return TypedResults.BadRequest("Visualize o Console para mais Informações");

           return TypedResults.Ok(storage);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
       
    }

    public static async Task<Results<Ok<StorageDTO>,BadRequest<string>>> UpdateIsValid
                                           (StorageServices storageServices,
                                            [FromQuery]
                                            bool isvalid,
                                            [FromQuery]
                                            Guid Id
                                            )
    {

        try
        {
            var storage = await storageServices.UpdateIsValid(Id, isvalid);

            if (storage == null)
                return TypedResults.BadRequest("Visualize o Console para mais Informações");

            return TypedResults.Ok(storage);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        } 
            
    }

    public static async Task<Results<Ok, BadRequest<string>>> DeleteStorage
                                                        (StorageServices storageServices,
                                                         Guid ID
                                                        )
    {
        try
        {
            var storage = await storageServices.DeleteStorage(ID);

            if (storage == null)
                return TypedResults.Ok();

            return TypedResults.BadRequest(storage);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.ToString());
        }
    }
}
