namespace loja_api.Entities;

public class Auditable
{
    public Guid CreatebyId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid UpdatebyId { get; set; }

    public DateTime UpdateDate { get; set; }
}
