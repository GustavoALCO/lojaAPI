using loja_api.Entities.auxiliar;

namespace loja_api.Mapper.Storage;

public class StorageDTO
{
    public Guid IdStorage { get; set; }

    public Guid IdProducts { get; set; }

    public int Quantity { get; set; }

    public double PriceBuy { get; set; }

    public Auditable Auditable { get; set; }
}
