using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Cupom;
using loja_api.Services;
using loja_api.Validators.Cupom;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace loja_api.EndpointsHandlers;

public class Cupomhandler
{

    public static async Task<Results<BadRequest<string>,Ok<IEnumerable<CupomDTO>>>> GetCupons
                                                                            (CupomService cupomService,
                                                                            [FromQuery]
                                                                            string Name)
    {
        try
        {
            var cupom = await cupomService.GetCupom(Name);

            if (cupom == null)
                return TypedResults.BadRequest("Não foi encontrado nenhum cupom");

            return TypedResults.Ok(cupom);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message.ToString());
        }
    }

    public static async Task<Results<BadRequest<string>, Ok<CupomDTO>>> CreateCupom
                                                      (CupomService cupomService,
                                                       [FromBody] CupomCreateDTO cupomCreate
                                                        )
    {
        try
        {
            var cupom = await cupomService.CreateCupom(cupomCreate);

            if (cupom == null)
                return TypedResults.BadRequest("Não foi possivel Ceiar um novo cupom. Verifique o Log");

            return TypedResults.Ok(cupom);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message.ToString());
        }
    }

    public static async Task<Results<BadRequest<string>, Ok<CupomDTO>>> UpdateCupom
                                                      (CupomService cupomService,
                                                        [FromBody]
                                                        CupomUpdateDTO cupomUpdate
                                                        )
    {
        try
        {
            var cupom = await cupomService.UpdateCupom(cupomUpdate);

            if (cupom == null)
                return TypedResults.BadRequest("Não foi possivel Ceiar um novo cupom. Verifique o Log");

            return TypedResults.Ok(cupom);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message.ToString());
        }
    }


    public static async Task<Results<Ok<string>, BadRequest<string>>> DeleteCupom(
                                                        Guid Id,
                                                        CupomService cupomService)
    {
        try
        {
            var cupom = await cupomService.DeleteCupom(Id);

            if (cupom == null)
                return TypedResults.BadRequest("Não foi possivel Ceiar um novo cupom. Verifique o Log");

            return TypedResults.Ok(cupom);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message.ToString());
        }

    }
}
