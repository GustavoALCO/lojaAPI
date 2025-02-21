using System.ComponentModel.DataAnnotations;
using loja_api.Entities.auxiliar;

namespace loja_api.Entities;

public class Cupom
{
    [Key]
    public Guid CupomId { get; set; } 

    public string Name { get; set; }

    public int quantity { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Auditable Auditable { get; set; }
}
