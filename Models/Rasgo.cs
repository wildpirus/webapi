using System.Text.Json.Serialization;

namespace webapi.Models;

public class Rasgo {
    public Guid rasgo_id {get; set;}
    public string titulo {get;set;}
    public TipoRasgo tipo_rasgo {get; set;}
    
    [JsonIgnore]
    public virtual ICollection<RasgoSuper> RasgosSuper {get; set;}
}

public enum TipoRasgo {
    Habilidad,
    Debilidad
}