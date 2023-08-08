using Proyecto.PiteApi.Models;

namespace Proyecto.PiteApi.Dtos;

public class UpdatePersonUserDto: User
{
    public new string  FirstName { get; set; }
    public new string LastName { get; set; }
    public new string Email { get; set; }
    public new System.Nullable<DateTime> Birthday { get; set; }
}
