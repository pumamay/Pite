using Proyecto.PiteApi.Interfaces;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using System.Text.Json.Serialization;

namespace Proyecto.PiteApi.Models;

public class EntityDeleTable : IDeletableEntity
{
    [JsonPropertyOrder(-2)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [JsonPropertyOrder(3)]
    [JsonIgnore]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [JsonIgnore]
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeletable { get; set; } = true;
}
