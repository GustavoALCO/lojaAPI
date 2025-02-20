using System.ComponentModel.DataAnnotations;

namespace loja_api.Entities;

public class Products
{
    [Key]
    public Guid IdProducts { get; set; }

    public string ProductName { get; set; }

    public string ProductDescription { get; set; }

    public string CodeProduct { get; set; }

    public string TypeProduct { get; set; }

    public double Price { get; set; }

    public int QuantityStorage {  get; set; }

    public ICollection<Storage> Storages { get; set; }

    public Auditable Auditable { get; set; }

}
