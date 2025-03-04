namespace loja_api.Mapper.Product;

public interface SearchProducts
{
    public string ProductName { get; set; }

    public string CodeProduct { get; set; }

    public string TypeProduct { get; set; }

    public double[] Price { get; set; }
}
