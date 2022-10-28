namespace webapi.Models;

public class Patrocinador {
    public Guid patrocinador_id {get; set;}
    public Guid super_id {get;set;}
    public string nombre {get; set;}
    public float monto {get; set;}
    public string origen_monto {get; set;}
    public virtual Super Super {get; set;}
}