using loja_api.Entities;

namespace loja_api.Mapper.Storage;

public class StorageCreateDTO
{

    public Guid IdProducts { get; set; }

    public int Quantity { get; set; }

    public double PriceBuy { get; set; }

    public int CreatebyId { get; set; }

    public bool IsValid { get; set; }

    public DateTime CreateDate { get; set; }
}
