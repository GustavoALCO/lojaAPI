using loja_api.Entities.auxiliar;

namespace loja_api.Mapper.Product;

public class ProductsCreateDTO
{
    public Guid IdProducts { get; set; }

    public string ProductName { get; set; }

    public string ProductDescription { get; set; }

    public string CodeProduct { get; set; }

    public string TypeProduct { get; set; }

    public double Price { get; set; }

    public int CreatebyId { get; set; }

    public DateTime CreateDate { get; set; }
}
