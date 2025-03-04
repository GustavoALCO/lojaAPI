using FluentValidation;
using loja_api.Mapper.Product;

namespace loja_api.Validators.Products;

public class UpdateProductsValidation : AbstractValidator<ProductsUpdateDTO>
{
    public UpdateProductsValidation()
    {
        RuleFor(p => p.UpdatebyId)
            .NotEmpty()
            .WithMessage("Deve Passar o Id de quem alterou por ultimo");

        RuleFor(p => p.UpdateDate)
            .NotEmpty()
            .WithMessage("O valor da data deve ser nulo");
    }
}
