using FluentValidation;
using loja_api.Mapper.User;

namespace loja_api.Validators.User;

public class CreateUserValidation : AbstractValidator<CreateUserDTO>
{
    public CreateUserValidation()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("É Obrigatorio passar um nome para Criar o usuario");

        RuleFor(u => u.Surname)
            .NotEmpty().WithMessage("É Obrigatorio passar um sobrenome para Criar o usuario");

        RuleFor(u => u.Cpf)
            .NotEmpty().WithMessage("É Necessario passar um Cpf para a Criação de um usuario")
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage("O CPF deve estar no formato 000.000.000-00"); 

        RuleFor(u => u.Cep)
            .NotEmpty().WithMessage("É Obrigatorio passar um CEP para Criar o usuario")
            .Matches(@"^\d{5}-\d{3}").WithMessage("O CEP deve estar no formato 00000-000.");
            
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("É Obrigatorio passar um Email para Criar o usuario")
            .EmailAddress().WithMessage("É Obrigatorio que passe um Email valido");
    }
}
