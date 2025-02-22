using FluentValidation;
using loja_api.Mapper.Cupom;

namespace loja_api.Validators.Cupom;

public class UpdateCupomValidation : AbstractValidator<CupomUpdateDTO>
{

    public UpdateCupomValidation()
    {
        RuleFor(c => c.CupomId)
            .NotEmpty().WithMessage("É obrigatorio passar um ID");

        RuleFor(Cupom => Cupom.UpdatebyId)
            .NotEmpty().WithMessage("Deve Possuir um Criador");
    }
}
