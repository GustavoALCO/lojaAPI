namespace loja_api.Entities;

public class User
{
    public Guid IdUser { get; set; }

    public string Name { get; set; }    

    public string Surname { get; set; }

    public string Cpf {  get; set; }

    public string Cep { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string password { get; set; }
}
