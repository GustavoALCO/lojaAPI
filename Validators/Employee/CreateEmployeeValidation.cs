using FluentValidation;
using loja_api.Mapper.Emploree;

namespace loja_api.Validators.Employee;

public class CreateEmployeeValidation : AbstractValidator<EmployeeCreateDTO>
{
    public CreateEmployeeValidation()
    {

        RuleFor(e => e.Id)
            .Empty().WithMessage("Id é necessario deixar Vazio");

        RuleFor(u => u.IsActive)
            .Empty().WithMessage("Necessario deixar IsValid vazio");

        RuleFor(u => u.Login)
            .NotEmpty().WithMessage("Necessario passar um Login para Criar o Funcionario");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Necessario passar uma senha");

        RuleFor(u => u.Position)
            .NotEmpty().WithMessage("Selecione Um cargo para o Funcionario");

        RuleFor(u => u.CreatebyId)
            .NotEmpty().WithMessage("É Obrigatorio passar o id de um criador");
    }
}
