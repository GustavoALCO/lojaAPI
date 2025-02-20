namespace loja_api.Entities;

public class Cupom
{
    public int Id { get; set; } 

    public string Name { get; set; }

    public int quantity { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Auditable Auditable { get; set; }
}
