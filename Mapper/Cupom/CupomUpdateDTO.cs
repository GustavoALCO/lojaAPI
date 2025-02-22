namespace loja_api.Mapper.Cupom;

public class CupomUpdateDTO
{
    public Guid CupomId { get; set; }

    public string Name { get; set; }

    public int Discount { get; set; }

    public int Quantity { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int UpdatebyId { get; set; }

    public DateTime UpdateDate { get; set; }
}
