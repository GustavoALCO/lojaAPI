using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Cupom;
using loja_api.Mapper.Emploree;
using loja_api.Mapper.User;
using Microsoft.EntityFrameworkCore;

namespace loja_api.Services;

public class EmployeeService
{
    private readonly ContextDB _DB;

    private readonly IMapper _mapper;

    private readonly ILogger _logger;

    private readonly IValidator<EmployeeCreateDTO> _validatorCreated;

    private readonly IValidator<EmployeeUpdateDTO> _validatorUpdate;

    private readonly HashService _hashService;

    public EmployeeService(ContextDB DB, IMapper mapper, ILogger<CupomService> logger, IValidator<EmployeeCreateDTO> validatorCreated, IValidator<EmployeeUpdateDTO> validatorUpdate, HashService hashService)
    {
        _DB = DB;
        _mapper = mapper;
        _logger = logger;
        _validatorCreated = validatorCreated;
        _validatorUpdate = validatorUpdate;
        _hashService = hashService;
    }

    public async Task<IEnumerable<EmployeeDTO>?> GetEmployee(string login)
    {
        var employee = _mapper.Map<IEnumerable<EmployeeDTO>>(await _DB.Employee.Where(e => login == null || e.Login.ToLower().Contains(login.ToLower())).ToListAsync());

        if (employee == null)
            return null;

        return employee;
    }

    public async Task<EmployeeDTO?> GetEmployeeID(int Id)
    {
        var employee = _mapper.Map<EmployeeDTO>(await _DB.Employee.FirstOrDefaultAsync(e => e.Id == Id));

        if (employee == null)
            return null;

        return employee;
    }

    public async Task<EmployeeDTO?> CreateEmployee(EmployeeCreateDTO createDTO)
    {
        //passa os parametros para a Validacao 
        var validation = _validatorCreated.Validate(createDTO);
        if (!validation.IsValid)
        {   //se nao for valido coleta os erros do Fluent Validation
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Funcionario: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

           //Retorna NULL 
            return null;
        }

        var loginValidation = _DB.Employee.FirstOrDefaultAsync(e => e.Login.ToUpper() == createDTO.Login.ToUpper());

        if (loginValidation == null)
        {
            _logger.LogWarning("Falha na Validação, Login já esta sendo usado");
            return null;
        }
            

        var employee = _mapper.Map<Employee>(createDTO);

        await _DB.Employee.AddAsync(employee);

        await _DB.SaveChangesAsync();

        return _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task<EmployeeDTO?> UpdateEmployee(EmployeeUpdateDTO UpdateDTO)
    {
        var validation = _validatorUpdate.Validate(UpdateDTO);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Funcionario: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return null;
        }

        var employee = _DB.Employee.FirstOrDefaultAsync(e => e.Id == UpdateDTO.Id);

        _mapper.Map(employee, UpdateDTO);

        await _DB.SaveChangesAsync();

        var valueReturn = await GetEmployeeID(employee.Id);

        return valueReturn;
    }

    public async Task<string> DeleteEmployee(int id)
    {
        var employee = await _DB.Employee.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
            return null;

        _DB.Remove(id);

        _DB.SaveChanges();

        return "Funcionario Deletado do Banco de dados";
    }

    public async Task<bool> LoginEmployee(EmployeeLoginDTO loginrequest)
    {
        try
        {
            var Employee = await _DB.Users.FirstOrDefaultAsync(c => c.Email == loginrequest.Login);

            var password = _hashService.ValidatePassword(Employee, loginrequest.Password);

            if (Employee == null || password == false)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Erro no EmployeeService: {ex.ToString()}");
            return false;
        }
    }

    public async Task<EmployeeDTO?> UpdateLogin(EmployeeLoginDTO loginDTO)
    {

        try
        {
            var User = await _DB.Employee.FirstOrDefaultAsync(c => c.Login == loginDTO.Login);

            if (User == null)
            {
                _logger.LogWarning("Usuário com e-mail {Email} não encontrado.", loginDTO.Login);
                return null;
            }

            if (loginDTO.Password != null)
            {
                _hashService.CreateHashEmployee(User, loginDTO.Password);
            }

            _mapper.Map(loginDTO, User);
            //Salva no banco de dados o Usuario Criado
            await _DB.SaveChangesAsync();
            //mapeia o usuario Criado Em um DTO para esconder variaveis importantes
            var returnUser = _mapper.Map<EmployeeDTO>( await GetEmployee(loginDTO.Login));

            return returnUser;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Erro no UserService: {ex.ToString()}");
            return null;
        }
    }
}
