using System.Text.Json.Serialization;

namespace Proyecto.PiteApi.Dtos;

public class UserDto
{
    [JsonPropertyOrder(0)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}
