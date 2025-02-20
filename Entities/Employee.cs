namespace loja_api.Entities;

public class Employee
{

    public int Id { get; set; } 

    public string FullName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public Boolean IsActive { get; set; }

    Auditable Auditable { get; set; }
}
