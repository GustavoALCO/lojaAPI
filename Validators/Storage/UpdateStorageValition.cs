using FluentValidation;
using loja_api.Mapper.Storage;
using System.Data;

namespace loja_api.Validators.Storage;

public class UpdateStorageValition : AbstractValidator<StorageUpdateDTO>
{
    public UpdateStorageValition()
    {

        RuleFor(c => c.IdStorage)
            .NotEmpty().WithMessage("É Necesserio Passar o Id ");

        RuleFor(c => c.UpdatebyId)
            .NotEmpty().WithMessage("É Necessario Ser passado o Id de quem esta alterando");

    }
}
