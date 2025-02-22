using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Cupom;
using loja_api.Validators.Cupom;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace loja_api.EndpointsHandlers;

public class Cupomhandler
{

    public static async Task<Results<NotFound,Ok<IEnumerable<CupomDTO>>>> GetCupons
                                                                            (IMapper mapper,
                                                                            ContextDB DB,
                                                                            [FromQuery(Name = "Name")]
                                                                            string? Name
                                                                            )
    {
            //Fazendo Chamado no banco de dados com duas funções, 1º Se name for nulo ele busca todos os Cupons
                                                                //2ºbusca todos os cupons que contem o valor passado na variavel
           var Cupom = mapper.Map<IEnumerable<CupomDTO>>(await DB.Cupom.Where(c => Name == null ||
                                                                              Name.ToUpper().Contains(c.Name.ToUpper()))
                                                                              .ToListAsync());
        
            //Se a variavel Cupom tiver menos de 1 objeto dentro dela retorna um NotFound
            if (!Cupom.Any())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(Cupom);
    }

    public static async Task<Results<BadRequest<string>, Ok<string>>> CreateCupom
                                                      (IMapper mapper,
                                                       ContextDB DB,
                                                       IValidator<CupomCreateDTO> validator,
                                                       [FromServices] ILogger<CreateCupomValidation> logger,
                                                       [FromBody] CupomCreateDTO cupomCreate
                                                        )
    {
        //Validando as Principais variaveis para a criação de um cupom 
        var validation = validator.Validate(cupomCreate);
        if (!validation.IsValid) 
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return TypedResults.BadRequest("Verifique o Console para uma Informação mais detalhada");
        }
            

        //Adicionando Data atual para o cupom sendo criado
        cupomCreate.CreateDate = DateTime.Now;

        //Converte o DTO para a Classe Cupom existente no banco de dados 
        var moto = mapper.Map<Cupom>(cupomCreate);

        //Adiciona no banco de dados 
        await DB.AddAsync( moto );

        //Salva as alterações 
        await DB.SaveChangesAsync();

        //retorna 200 com uma mensagem estatica de usuario criado 
        return TypedResults.Ok("Cupom Criado Com Sucesso");
    }

    public static async Task<Results<BadRequest<string>, Ok>> UpdateCupom
                                                      (IMapper mapper,
                                                        IValidator<CupomUpdateDTO> validator,
                                                        [FromServices] ILogger<UpdateCupomValidation> logger,
                                                        ContextDB DB,
                                                        [FromBody]
                                                        CupomUpdateDTO cupomUpdate
                                                        )
    {
        var validation = validator.Validate(cupomUpdate);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return TypedResults.BadRequest("Verifique o Console para uma Informação mais detalhada");
        }


        //Busca No banco de dados o Cupom passado pelo Id 
        var cupom = await DB.Cupom.FirstOrDefaultAsync(c => c.CupomId == cupomUpdate.CupomId);

        //Verifica se teve algum retorno Valido
        if (cupom == null)
            return TypedResults.BadRequest("Id Não Identificado");

        //adicionan
        cupomUpdate.UpdateDate = DateTime.Now;

        //Substitui os dados do Cupom antigo pelos passados por ultimo 
        mapper.Map(cupomUpdate,cupom);

        //Salva no banco de dados 
        await DB.SaveChangesAsync();

        //Retorna com um Status 200
        return TypedResults.Ok();
    }


    public static async Task<Results<Ok, BadRequest>> DeleteCupom(
                                                        Guid Id,
                                                        ContextDB Db)
    {

        var cupom = await Db.Cupom.FirstOrDefaultAsync(c => c.CupomId == Id);

        if (cupom == null)
            return TypedResults.BadRequest();

        Db.Remove(cupom);

        Db.SaveChanges();

        return TypedResults.Ok();
    }
}
