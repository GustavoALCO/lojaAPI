namespace loja_api.Mapper.Storage;

public class StorageUpdateDTO
{
    public Guid IdStorage { get; set; }

    public Guid IdProducts { get; set; }

    public int Quantity { get; set; }

    public double PriceBuy { get; set; }

    public int UpdatebyId { get; set; }

    public bool IsValid { get; set; }

    public DateTime UpdateDate { get; set; }
}

