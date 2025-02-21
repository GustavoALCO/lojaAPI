namespace loja_api.Mapper.Product;

public class ProductsUpdateDTO
{
    public string ProductName { get; set; }

    public string ProductDescription { get; set; }

    public string CodeProduct { get; set; }

    public string TypeProduct { get; set; }

    public double Price { get; set; }

    public int UpdatebyId { get; set; }

    public DateTime UpdateDate { get; set; }
}
