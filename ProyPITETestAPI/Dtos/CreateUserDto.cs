﻿namespace Proyecto.PiteApi.Dtos;

public class CreateUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; } 
    public string Password { get; set; }
    public System.Nullable<DateTime> Birthday { get; set; }
}
