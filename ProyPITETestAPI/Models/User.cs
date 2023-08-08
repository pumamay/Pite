using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Proyecto.PiteApi.Models
{
    public class User: Entity
    {
        [JsonPropertyOrder(1)]
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        [JsonIgnore]
        public string Password { get; set; }
        public System.Nullable<DateTime> Birthday { get; set; }
    }
}
