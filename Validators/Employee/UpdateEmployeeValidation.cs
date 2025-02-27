using FluentValidation;
using loja_api.Mapper.Emploree;

namespace loja_api.Validators.Employee;

public class UpdateEmployeeValidation : AbstractValidator<EmployeeUpdateDTO>
{
    public UpdateEmployeeValidation()
    {
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage("É necessario passar o id para Buscar o Funcionario");


        RuleFor(c => c.UpdatebyId)
            .NotEmpty().WithMessage("É necessario passar o id para o historico de quem alterou Funcionario");
    }
}
