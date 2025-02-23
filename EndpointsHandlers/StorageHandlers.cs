using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Storage;
using loja_api.Validators.Storage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace loja_api.EndpointsHandlers;

public static class StorageHandlers
{

    public static async Task<Results<NotFound,Ok<IEnumerable<StorageDTO>>>> GetStorage
                                                                            (ContextDB Db,
                                                                             IMapper mapper,
                                                                             Guid? ID)
    {
         var Storage = mapper.Map<IEnumerable<StorageDTO>>(await  Db.Storage.Where(s => ID == null || s.IdProducts == ID).ToListAsync());

        if(!Storage.Any())
            return TypedResults.NotFound();

        return TypedResults.Ok(Storage);
    }

    public static async Task<Results<Ok<StorageDTO>, NotFound>> GetStorageId
                                                        (ContextDB DB,
                                                        IMapper mapper,
                                                        Guid id 
                                                        )
    {

        var Storage = mapper.Map<StorageDTO>( await DB.Storage.FirstOrDefaultAsync(s => s.IdStorage.Equals(id)));  

        if(Storage == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(Storage);
    }

    public static async Task<Results<Ok<string>,BadRequest<string>>> CreateStorage
                                                            (ContextDB DB,
                                                             IMapper mapper,
                                                             IValidator<StorageCreateDTO> validator,
                                                             [FromServices] ILogger<CreateStorageValidation> logger,
                                                             [FromBody]
                                                             StorageCreateDTO createDTO)
    {

        var validation = validator.Validate(createDTO);
        if(!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return TypedResults.BadRequest("Verifique o Console para uma Informação mais detalhada");
        }

        //Adicionando Data atual para o cupom sendo criado
        createDTO.CreateDate = DateTime.Now;
        //Mapeia o CreateDTO para Storage
        var storage = mapper.Map<Storage>(createDTO);
        //Adiciona ao Banco de dados 
        await DB.AddAsync(storage);
        //Salva no banco de dados 
        await DB.SaveChangesAsync();
        //Retorna Status 200 Com uma mensagem de Storage Criado 
        return TypedResults.Ok("Storage Criado Com Sucesso");
    }

    public static async Task<Results<Ok<string>, BadRequest<string>>> UpdateStorage(
                                                                       IMapper mapper,
                                                                       IValidator<StorageUpdateDTO> validator,
                                                                       ILogger<UpdateStorageValition> logger,
                                                                       ContextDB DB,
                                                                       [FromBody]
                                                                       StorageUpdateDTO UpdateDTO)
    {

        var validation = validator.Validate(UpdateDTO);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return TypedResults.BadRequest("Verifique o Console para uma Informação mais detalhada");
        }

        var store = await DB.Storage.FirstOrDefaultAsync(s => s.IdStorage == UpdateDTO.IdStorage);

        if (store == null)
            return TypedResults.BadRequest("Erro ao Encontrar ID");

        mapper.Map(UpdateDTO, store);

        await DB.SaveChangesAsync();

        return TypedResults.Ok("Store Atualizado Com Sucesso");
    }

    public static async Task<Results<Ok,BadRequest>> UpdateIsValid
                                           (ContextDB DB,
                                            [FromQuery]
                                            bool isvalid,
                                            [FromQuery]
                                            Guid Id
                                            )
    {
        var storage = await DB.Storage.FirstOrDefaultAsync(s => s.IdStorage == Id);

        if(storage == null)
            return TypedResults.BadRequest();

        storage.IsValid = isvalid;

        DB.Update(storage);

        await DB.SaveChangesAsync();

        return TypedResults.Ok();

    }

    public static async Task<Results<Ok, BadRequest>> DeleteStorage
                                                        (ContextDB DB,
                                                         Guid ID
                                                        )
    {
        var Storage = DB.Storage.FirstOrDefault( s => s.IdStorage == ID);

        if(Storage == null)
            return TypedResults.BadRequest();

        DB.Remove(Storage);

        await DB.SaveChangesAsync();

        return TypedResults.Ok();
    }
}
