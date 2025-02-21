namespace loja_api.Mapper.Cupom;

public class CupomCreateDTO
{
    public string Name { get; set; }

    public int Quantity { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int CreatebyId { get; set; }

    public DateTime CreateDate { get; set; }
}
