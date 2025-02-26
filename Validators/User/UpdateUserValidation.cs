using FluentValidation;
using loja_api.Mapper.User;

namespace loja_api.Validators.User;

public class UpdateUserValidation : AbstractValidator<UserUpdateDTO>
{
    public UpdateUserValidation()
    {
        RuleFor(c => c.IdUser)
            .NotEmpty().WithMessage("É Necesserio Passar o Id ");
    }
}
