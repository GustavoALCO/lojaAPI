using loja_api.Entities.auxiliar;

namespace loja_api.Mapper.Emploree;

public class EmployeeCreateDTO
{

    public string FullName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Position { get; set; }

    public bool IsActive { get; set; }

    public int CreatebyId { get; set; }

    public DateTime CreateDate { get; set; }
}
