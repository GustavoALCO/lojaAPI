using loja_api.Entities;
using System.ComponentModel.DataAnnotations;

namespace loja_api.Mapper.User;

public class UserUpdateDTO
{
    public Guid IdUser { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Cep { get; set; }

    public string PhoneNumber { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool IsValid { get; set; }
}
