using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Cupom;
using loja_api.Mapper.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace loja_api.Services;

public class CupomService
{

    private readonly ContextDB _DB;

    private readonly IMapper _mapper;

    private readonly ILogger _logger;

    private readonly IValidator<CupomCreateDTO> _validatorCreated;

    private readonly IValidator<CupomUpdateDTO> _validatorUpdate;
    public CupomService(ContextDB DB, IMapper mapper, ILogger logger, IValidator<CupomCreateDTO> validatorCreated, IValidator<CupomUpdateDTO> validatorUpdate)
    {
        _DB = DB;
        _mapper = mapper;
        _logger = logger;
        _validatorCreated = validatorCreated;
        _validatorUpdate = validatorUpdate;
    }

    public async Task<IEnumerable<CupomDTO>?> GetCupom(string Name)
    {
        //Fazendo Chamado no banco de dados com duas funções, 1º Se name for nulo ele busca todos os Cupons
        //2ºbusca todos os cupons que contem o valor passado na variavel
        var Cupom = _mapper.Map<IEnumerable<CupomDTO>>(await _DB.Cupom.Where(c => Name == null ||
                                                                           Name.ToUpper().Contains(c.Name.ToUpper()))
                                                                           .ToListAsync());

        //Se a variavel Cupom tiver menos de 1 objeto dentro dela retorna um NotFound
        if (!Cupom.Any())
        {
            return null;
        }

        return Cupom;
    }

    public async Task<CupomDTO?> GetCupomId(Guid id)
    {
        var cupom = _mapper.Map<CupomDTO>(await _DB.Cupom.FirstOrDefaultAsync(c => c.CupomId == id));

        if(cupom == null)
            return null;

        return cupom;
    }

    public async Task<CupomDTO> CreateCupom(CupomCreateDTO cupomCreate)
    {
        //Validando as Principais variaveis para a criação de um cupom 
        var validation = _validatorCreated.Validate(cupomCreate);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return null;
        }


        //Adicionando Data atual para o cupom sendo criado
        cupomCreate.CreateDate = DateTime.Now;

        cupomCreate.CupomId = Guid.NewGuid();

        //Converte o DTO para a Classe Cupom existente no banco de dados 
        var cupom = _mapper.Map<Cupom>(cupomCreate);

        //Adiciona no banco de dados 
        await _DB.AddAsync(cupom);

        //Salva as alterações 
        await _DB.SaveChangesAsync();

        var valuereturn = await GetCupomId(cupomCreate.CupomId);

        return valuereturn;
    }

    public async Task<CupomDTO?> UpdateCupom(CupomUpdateDTO cupomUpdate)
    {
        var validation = _validatorUpdate.Validate(cupomUpdate);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return null;
        }


        //Busca No banco de dados o Cupom passado pelo Id 
        var cupom = await _DB.Cupom.FirstOrDefaultAsync(c => c.CupomId == cupomUpdate.CupomId);

        //Verifica se teve algum retorno Valido
        if (cupom == null)
            return null;

        //adicionan
        cupomUpdate.UpdateDate = DateTime.Now;

        //Substitui os dados do Cupom antigo pelos passados por ultimo 
        _mapper.Map(cupomUpdate, cupom);

        //Salva no banco de dados 
        await _DB.SaveChangesAsync();

        var valueReturn = await GetCupomId(cupomUpdate.CupomId);

        return valueReturn;
    }

    public async Task<string> DeleteCupom(Guid Id)
    {
        var cupom = await _DB.Cupom.FirstOrDefaultAsync(c => c.CupomId == Id);

        if (cupom == null)
            return null;

        _DB.Remove(cupom);

        _DB.SaveChanges();

        return "Cupom Excluido COm Sucesso";
    }
}
