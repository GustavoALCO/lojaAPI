namespace loja_api.Mapper.Emploree;

public class EmployeeUpdateDTO
{
    public string FullName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Position { get; set; }

    public bool IsActive { get; set; }

    public int UpdatebyId { get; set; }

    public DateTime UpdateDate { get; set; }
}
