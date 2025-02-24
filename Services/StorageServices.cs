using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Storage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace loja_api.Services;

public class StorageServices
{
    private readonly ContextDB _DB;

    private readonly IMapper _mapper;

    private readonly ILogger _logger;

    private readonly IValidator<StorageCreateDTO> _validatorCreated;

    private readonly IValidator<StorageUpdateDTO> _validatorUpdate;
    public StorageServices(ContextDB DB, IMapper mapper, ILogger logger, IValidator<StorageCreateDTO> validatorCreated, IValidator<StorageUpdateDTO> validatorUpdate)
    {
        _DB = DB;
        _mapper = mapper;
        _logger = logger;
        _validatorCreated = validatorCreated;
        _validatorUpdate = validatorUpdate;
    }

    public async Task<IEnumerable<StorageDTO>?> GetStorage(Guid? ID)
    {
        var Storage = _mapper.Map<IEnumerable<StorageDTO>>(await _DB.Storage.Where(s => ID == null || s.IdProducts == ID).ToListAsync());

        if (!Storage.Any())
            return null;

        return Storage;
    }

    public async Task<StorageDTO?> GetStorageID(Guid id)
    {
        var Storage = _mapper.Map<StorageDTO>(await _DB.Storage.FirstOrDefaultAsync(s => s.IdStorage.Equals(id)));

        if (Storage == null)
            return null;

        return Storage;
    }

    public async Task<StorageDTO?> CreateStorage(StorageCreateDTO createDTO)
    {
        var validation = _validatorCreated.Validate(createDTO);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            return null;
        }

        //Adicionando Data atual para o cupom sendo criado
        createDTO.CreateDate = DateTime.Now;
        //Mapeia o CreateDTO para Storage
        var storage = _mapper.Map<Storage>(createDTO);
        //Adiciona ao Banco de dados 
        await _DB.AddAsync(storage);
        //Salva no banco de dados 
        await _DB.SaveChangesAsync();
  
        var valueReturn = _mapper.Map<StorageDTO>(createDTO);

        return valueReturn;
    }


   public async Task<StorageDTO?> UpdateStorage(StorageUpdateDTO UpdateDTO)
    {
        var validation = _validatorUpdate.Validate(UpdateDTO);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return null;
        }

        var store = _DB.Storage.FirstOrDefaultAsync(s => s.IdStorage == UpdateDTO.IdStorage);

        if (store == null)
            return null;

        await _mapper.Map(UpdateDTO, store);
        await _DB.SaveChangesAsync();

        var valueReturn = await GetStorageID(UpdateDTO.IdStorage);

        return valueReturn;
    }

    public async Task<StorageDTO?> UpdateIsValid(Guid Id, bool isvalid)
    {
        var storage = await _DB.Storage.FirstOrDefaultAsync(s => s.IdStorage == Id);

        if (storage == null)
            return null;

        storage.IsValid = isvalid;

        _DB.Update(storage);

        await _DB.SaveChangesAsync();

        var valueReturn = await GetStorageID(storage.IdStorage);

        return valueReturn;
    }

    public async Task<string?> DeleteStorage(Guid ID)
    {
        var Storage = _DB.Storage.FirstOrDefault(s => s.IdStorage == ID);

        if (Storage == null)
            return "Não Encontrado";

        _DB.Remove(Storage);

        await _DB.SaveChangesAsync();

        return null;
    }
}
