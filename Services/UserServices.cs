using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Cupom;
using loja_api.Mapper.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace loja_api.Services;

public class UserServices
{

    private readonly ContextDB _DB;

    private readonly IMapper _mapper;

    private readonly ILogger _logger;

    private readonly IValidator<CreateUserDTO> _validatorCreated;

    private readonly IValidator<UserUpdateDTO> _validatorUpdate;

    private readonly HashService _hashService;
    public UserServices(ContextDB DB, IMapper mapper, ILogger<UserServices> logger, IValidator<CreateUserDTO> validatorCreated, IValidator<UserUpdateDTO> validatorUpdate, HashService hashService)
    {
        _DB = DB;
        _mapper = mapper;
        _logger = logger;
        _validatorCreated = validatorCreated;
        _validatorUpdate = validatorUpdate;
        _hashService = hashService;
    }

    public async Task<IEnumerable<UserDTO>?> GetUser(string login)
    {
        var user = _mapper.Map<IEnumerable<UserDTO>>(await _DB.Users.Where(c => login == null|| c.Email.ToUpper().Contains(login.ToUpper())).ToListAsync());

        if (user == null)
            return null;

        return user;
    }

    public async Task<UserDTO?> GetUserID(Guid id)
    {
        var user = _mapper.Map<UserDTO>(await _DB.Users.FirstOrDefaultAsync(c => c.IdUser == id));

        if (user == null)
            return null;

        return user;
    }

    public async Task<UserDTO?> CreateUser(CreateUserDTO UserCreate)
    {
        var validation = _validatorCreated.Validate(UserCreate);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Usuario: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            return null;
        }

        try
        {   //Maperia o userCreate para User
            var User = _mapper.Map<User>(UserCreate);
            //Adiciona um guid ao Usuario Criado
            User.IdUser = Guid.NewGuid();
            
            User.IsValid = true;
            //Chama uma classe do HashService, onde ela cria um hash baseado no usuario e senha passada 
            _hashService.CreateHash(User,User.Password);
            //Adiciona no Banco de dados o Usuario
            _DB.Users.Add(User);
            //Salva no banco de dados o Usuario Criado
            await _DB.SaveChangesAsync();
            //mapeia o usuario Criado Em um DTO para esconder variaveis importantes
            var returnUser = _mapper.Map<UserDTO>(User);

            return returnUser;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Erro no UserService: {ex.ToString()}");
            return null;
        }
    }

    public async Task<string?> DeleteUser(Guid id)
    {
        var user = _mapper.Map<UserDTO>(await _DB.Users.FirstOrDefaultAsync(c => c.IdUser == id));

        if (user == null)
            return null;

        _DB.Remove(user);

        await _DB.SaveChangesAsync();

        return "Usuario Removido Com Sucesso";
    }

    public async Task<UserDTO?> UpdateUser(UserUpdateDTO UserUpdate)
    {
        var validation = _validatorUpdate.Validate(UserUpdate);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Usuario: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            return null;
        }

        
        var User = await _DB.Users.FirstOrDefaultAsync(c => c.IdUser == UserUpdate.IdUser);

        if (User == null)
        {
            _logger.LogWarning("Usuário ID {IdUser} não encontrado.", UserUpdate.IdUser);
            return null;
        }

        _mapper.Map(UserUpdate, User);
        //Salva no banco de dados o Usuario Criado
        await _DB.SaveChangesAsync();
        //mapeia o usuario Criado Em um DTO para esconder variaveis importantes
        var returnUser = _mapper.Map<UserDTO>(User);

        return returnUser;
        
        
    }

    public async Task<UserDTO?> UpdateLogin(UserLoginDTO loginDTO)
    {
        
        try
        {
            var User = await _DB.Users.FirstOrDefaultAsync(c => c.Email == loginDTO.Email);

            if (User == null)
            {
                _logger.LogWarning("Usuário com e-mail {Email} não encontrado.", loginDTO.Email);
                return null;
            }

            if (loginDTO.Password != null)
            {
                _hashService.CreateHash(User, loginDTO.Password);
            }

            _mapper.Map(loginDTO, User);
            //Salva no banco de dados o Usuario Criado
            await _DB.SaveChangesAsync();
            //mapeia o usuario Criado Em um DTO para esconder variaveis importantes
            var returnUser = _mapper.Map<UserDTO>(User);

            return returnUser;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Erro no UserService: {ex.ToString()}");
            return null;
        }
    }

    public async Task<bool>Login(UserLoginDTO loginDTO)
    {
        try
        {
            var User = await _DB.Users.FirstOrDefaultAsync(c => c.Email == loginDTO.Email);

            var password = _hashService.ValidatePassword(User, loginDTO.Password);

            if (User == null || password == false)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Erro no UserService: {ex.ToString()}");
            return false;
        }
    }
}
