using FluentValidation;
using loja_api.Mapper.Storage;

namespace loja_api.Validators.Storage;

public class CreateStorageValidation : AbstractValidator<StorageCreateDTO>
{

    public CreateStorageValidation()
    {
        RuleFor(s => s.IdProducts)
            .NotEmpty().WithMessage("É Necessario Passar o ID do produto");

        RuleFor(s => s.Quantity)
            .NotEmpty().InclusiveBetween(1,8000).WithMessage("Precisa Declarar um intervalo de valor entre 1 e 8000");

        RuleFor(s => s.PriceBuy)
            .NotEmpty();

        RuleFor(s => s.CreatebyId)
            .NotEmpty();

    }
}
