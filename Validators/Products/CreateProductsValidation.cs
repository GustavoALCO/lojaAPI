using FluentValidation;
using loja_api.Mapper.Product;

namespace loja_api.Validators.Products;

public class CreateProductsValidation : AbstractValidator<ProductsCreateDTO>
{

    public CreateProductsValidation()
    {
        RuleFor(c => c.IdProducts)
            .Empty()
            .WithMessage("ID deve estar vazio para a Criação de um Produto");

        RuleFor(c => c.ProductName)
            .NotEmpty()
            .WithMessage("Deve ser passado um nome para o produto")
            .Length(5, 50)
            .WithMessage("O nome deve ter um Intervalo de 5 a 50 caracteres");

        RuleFor(c => c.ProductDescription)
            .NotEmpty()
            .WithMessage("Deve ser passado uma descrição para o produto")
            .Length(20, 100)
            .WithMessage("A descrição deve ter um Intervalo de 20 a 100 caracteres");

        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("Deve Passar um preço para o produto");

        RuleFor(c => c.CreatebyId)
            .NotEmpty()
            .WithMessage("Deve Passar o ID do Usuario para criar o usuario");

        RuleFor(c => c.CreateDate)
            .Empty()
            .WithMessage("O valor de CreateData Deve estar Vazio");
    }
}
