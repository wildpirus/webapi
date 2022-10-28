namespace webapi.Models;

public class Pelea {
    public Guid pelea_id {get; set;}
    public Guid super_id {get;set;}
    public Guid enfrentamiento_id {get; set;}
    public bool gana {get; set;}
    public virtual Super Super {get; set;}
    public virtual Enfrentamiento Enfrentamiento {get; set;}
}