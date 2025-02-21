using FluentValidation;
using loja_api.Mapper.Cupom;

namespace loja_api.Validators.Cupom;

public class CreateCupomValidation : AbstractValidator<CupomCreateDTO>
{
    public CreateCupomValidation()
    {
        RuleFor(Cupom => Cupom.Name)
            .NotEmpty().WithMessage("É Obrigatorio Definir um nome ao Cupom para ser encontrado")
            .MaximumLength(10).WithMessage("O Nome Deve Conter no maximo 10 caracteres");

        RuleFor(Cupom => Cupom.Discount)
            .NotEmpty().WithMessage("É Obrigatorio Definir um Valor de Desconto ao Cupom")
            .LessThanOrEqualTo(20).WithMessage("O Desconto Não Pode Ser Maior Que 20%");

        RuleFor(Cupom => Cupom.ExpirationDate)
            .NotEmpty().WithMessage("Tem que ser passado uma data de validade para o cupom");

        RuleFor(Cupom => Cupom.Quantity)
            .NotEmpty().WithMessage("Deve Passar algum valor de quantidade minimo para que seja encontrado");

        RuleFor(Cupom => Cupom.CreatebyId)
            .NotEmpty().WithMessage("Deve Possuir um Criador");
    }
}
