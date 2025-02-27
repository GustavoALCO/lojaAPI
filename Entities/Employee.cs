﻿using System.ComponentModel.DataAnnotations;
using loja_api.Entities.auxiliar;

namespace loja_api.Entities;

public class Employee
{
    [Key]
    public int Id { get; set; } 

    public string FullName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Position { get; set; }

    public bool IsActive { get; set; }

    public Auditable Auditable { get; set; }
}
