using System.ComponentModel.DataAnnotations;

namespace loja_api.Entities;

public class User
{
    [Key]
    public Guid IdUser { get; set; }

    public string Name { get; set; }    

    public string Surname { get; set; }

    public string Cpf {  get; set; }

    public string Cep { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool EmailConfirmed { get; set; }

    public  ICollection<MarketCart> MarketCart { get; set; }
}
