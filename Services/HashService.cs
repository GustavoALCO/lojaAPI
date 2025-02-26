﻿using loja_api.Entities;
using Microsoft.AspNetCore.Identity;

namespace loja_api.Services;

public class HashService
{
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();



    public void CreateHash(User user, string password)
    {
        // Hash da senha antes de armazenar
        user.Password = _passwordHasher.HashPassword(user ,password);
        // Armazene o usuário no banco de dados (o armazenamento real deve ser feito em um repositório ou serviço de dados)
    }

    public bool ValidateUser(User user, string password)
    {
        // Verifica a senha fornecida
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return result == PasswordVerificationResult.Success;
    }
}
