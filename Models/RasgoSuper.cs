using System.Text.Json.Serialization;

namespace webapi.Models;

public class RasgoSuper {
    public Guid rasgo_super_id {get; set;}
    public Guid rasgo_id {get;set;}
    public Guid super_id {get;set;}
    public virtual Rasgo Rasgo {get ; set;}
    [JsonIgnore]
    public virtual Super Super {get; set;}
}

