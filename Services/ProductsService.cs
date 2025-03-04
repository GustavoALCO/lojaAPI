using AutoMapper;
using FluentValidation;
using loja_api.Context;
using loja_api.Entities;
using loja_api.Mapper.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace loja_api.Services;

public class ProductsService
{
    private readonly ContextDB _DB;

    private readonly IMapper _mapper;

    private readonly ILogger _logger;

    private readonly IValidator<ProductsCreateDTO> _validatorCreated;

    private readonly IValidator<ProductsUpdateDTO> _validatorUpdate;

    private readonly HashService _hashService;

    public ProductsService(ContextDB DB, IMapper mapper, ILogger<ProductsDTO> logger, IValidator<ProductsCreateDTO> validatorCreated, IValidator<ProductsUpdateDTO> validatorUpdate, HashService hashService)
    {
        _DB = DB;
        _mapper = mapper;
        _logger = logger;
        _validatorCreated = validatorCreated;
        _validatorUpdate = validatorUpdate;
        _hashService = hashService;
    }
    
    public async Task<IEnumerable<ProductsDTO>?> GetProducts(string? Name)
    {
        //Faz uma busca pelo nome e retorna um IEnumerable de ProdutosDTO
        var products = _mapper.Map<IEnumerable<ProductsDTO>>(await _DB.Products.Where(p => Name == null|| p.ProductName.ToLower().Contains(Name.ToLower())).ToListAsync());

        //Se o não achar nenhum produto manda uma observeção no console e retorna null
        if(!products.Any())
        {
            _logger.LogWarning("Produto Não Encontrado");
            return null;
        }

        // Busca todas as quantidades de produtos do estoque
        var storageData = await _DB.Storage
            .Where(s => s.IsValid)  // Busca produtos validos no estoque
            .GroupBy(s => s.IdProducts)  // Agrupa pelo Id do Produto
            .Select(g => new
            {
                IdProducts = g.Key,
                TotalQuantity = g.Sum(s => s.Quantity) // Soma as quantidades
            })
            .ToListAsync();

        // Cria um dicionário para acesso rápido
        var storageDict = storageData.ToDictionary(s => s.IdProducts, s => s.TotalQuantity);

        // Atribui a quantidade total ao Products
        foreach (var product in products)
        {
            product.QuantityStorage = storageDict.GetValueOrDefault(product.IdProducts, 0);
        }

        //Retorna um IEnumerable de ProdutosDTO
        return products;
    }

    public async Task<ProductsDTO?> GetProductsID(Guid Id)
    {
        //Busca e mapeia o Produto pelo Id 
        var products = _mapper.Map<ProductsDTO>(await _DB.Products.FirstOrDefaultAsync(p => p.IdProducts == Id));

        //Se Produto Retornar Null ele Retorna um Null e não faz outra chamada no banco de dados 
        if( products == null ) 
            return null;

        // Faz Mais uma chamada no Banco de dados para Procurar quantos Produtos tem no estoque
        var storage = await _DB.Storage.Where(p => p.IdProducts == Id && p.IsValid == true).ToListAsync();

        //Passa o valor de quantos Produtos tem no estoque para uma variavel 
        var quantityAll = storage.Select(s => s.Quantity).ToList();

        //Retorna a Soma de todos os Produtos para a Variavel QuantityStorage
        products.QuantityStorage = quantityAll.Sum();

        //Retorna o DTO de Product
        return products;
    }

    public async Task<IEnumerable<ProductsDTO>?> GetProducts(SearchProducts searchProducts)
    {
        var query = _DB.Products.AsQueryable();

        if (!searchProducts.ProductName.IsNullOrEmpty())
            query = query.Where(p => p.ProductName.ToLower().Contains(searchProducts.ProductName.ToLower()));

        if(!searchProducts.CodeProduct.IsNullOrEmpty())
            query = query.Where(p => p.CodeProduct == searchProducts.CodeProduct);

        if(searchProducts.Price != null && searchProducts.Price.Length == 2)
        {
            double startPrice = searchProducts.Price[0];
            double endPrice = searchProducts.Price[1];

            if(startPrice > endPrice)
                (startPrice, endPrice) = (endPrice, startPrice);

            query = query.Where(p => p.Price >= startPrice && p.Price <= endPrice);
        }

        var products = _mapper.Map<IEnumerable<ProductsDTO>>(await query.ToListAsync());

        if (products.IsNullOrEmpty())
        {
            _logger.LogWarning("Não foi encontrado nenhum produto");
            return null;
        }

        return products;
    }

    public async Task<ProductsDTO?> CreateProductsDTO(ProductsCreateDTO CreateDTO)
    {
        var validation = _validatorCreated.Validate(CreateDTO);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return null;
        }

        CreateDTO.IdProducts = Guid.NewGuid();

        CreateDTO.CreateDate = DateTime.UtcNow;

        var products = _mapper.Map<Products>(CreateDTO);

        await _DB.AddAsync(products);

        await _DB.SaveChangesAsync();

        return _mapper.Map<ProductsDTO>(products);
    }

    public async Task<ProductsDTO?> UpdateProducts(ProductsUpdateDTO UpdateDTO)
    {
        var validation = _validatorUpdate.Validate(UpdateDTO);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Cupom: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            //Retorna error 400 e pede para o adm verificar o console para mais informações 
            return null;
        }
        // Busca No Banco de dados Produto com o ID passado
        var products = _DB.Products.FirstOrDefaultAsync(c => c.IdProducts == UpdateDTO.IdProducts);
        // Declara a data que foi alterado
        UpdateDTO.UpdateDate = DateTime.UtcNow;
        // Substitui os dados pelos mais novos que foi passado
        _mapper.Map(products, UpdateDTO);
        // Salva as alteraçoes no banco de dados
        await _DB.SaveChangesAsync();
        // Faz uma nova busca e retorna ProductsDTO
        return await GetProductsID(UpdateDTO.IdProducts);
    }

    public async Task<bool> DeleteProducts(Guid id)
    {
        var products = _DB.Products.FirstOrDefaultAsync(p => p.IdProducts == id);

        if (products == null)
        {
            _logger.LogWarning("Não Foi possivel Encontrar o Produto");
             return false; 
        }

        _DB.Remove(products);

        await _DB.SaveChangesAsync();

        return true;
    }
    
}
