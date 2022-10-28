using System.Text.Json.Serialization;

namespace webapi.Models;

public class Enfrentamiento {
    public Guid enfrentamiento_id {get; set;}
    public string titulo {get;set;}
    public DateTime fecha {get; set;}
    
    [JsonIgnore]
    public virtual ICollection<Pelea> Peleas {get; set;}
}