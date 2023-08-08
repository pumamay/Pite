using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using Proyecto.PiteApi.Interfaces;
using System.Text.Json.Serialization;

namespace Proyecto.PiteApi.Models;

public class Entity: IDatedEntity
{
    [JsonPropertyOrder(-2)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [JsonPropertyOrder(3)]
    [JsonIgnore]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    [JsonIgnore]
    public System.Nullable<DateTime> UpdatedDate { get; set; }
    [JsonIgnore]
    public System.Nullable<DateTime> DeletedDate { get; set; }
}
