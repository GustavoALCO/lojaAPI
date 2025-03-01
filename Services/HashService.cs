using loja_api.Entities;
using Microsoft.AspNetCore.Identity;

namespace loja_api.Services;

public class HashService
{
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    private readonly PasswordHasher<Employee> _passwordHasherEmployee = new PasswordHasher<Employee>();

    public void CreateHashEmployee(Employee employee, string password)
    {
        // Hash da senha antes de armazenar
        employee.Password = _passwordHasherEmployee.HashPassword(employee, password);
        // Armazene o usuário no banco de dados (o armazenamento real deve ser feito em um repositório ou serviço de dados)
    }

    public void CreateHash(User user, string password)
    {
        // Hash da senha antes de armazenar
        user.Password = _passwordHasher.HashPassword(user ,password);
        // Armazene o usuário no banco de dados (o armazenamento real deve ser feito em um repositório ou serviço de dados)
    }

    public bool ValidatePasswordEmployee(Employee employee, string password)
    {
        // Verifica a senha fornecida
        var result = _passwordHasherEmployee.VerifyHashedPassword(employee, employee.Password, password);
        return result == PasswordVerificationResult.Success;
    }

    public bool ValidatePassword(User user, string password)
    {
        // Verifica a senha fornecida
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return result == PasswordVerificationResult.Success;
    }
}
