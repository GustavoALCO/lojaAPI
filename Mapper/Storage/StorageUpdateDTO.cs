namespace loja_api.Mapper.Storage;

public class StorageUpdateDTO
{
    public Guid IdProducts { get; set; }

    public int Quantity { get; set; }

    public double PriceBuy { get; set; }

    public int UpdatebyId { get; set; }

    public DateTime UpdateDate { get; set; }
}

