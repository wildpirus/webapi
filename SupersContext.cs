using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi;

public class SupersContext: DbContext{
    public DbSet<Super> Supers {get; set;}
    public DbSet<RasgoSuper> RasgosSupers {get; set;}
    public DbSet<Rasgo> Rasgos {get; set;}
    public DbSet<Pelea> Peleas {get; set;}
    public DbSet<Enfrentamiento> Enfrentamientos {get; set;}
    public DbSet<Patrocinador> Patrocinadores {get; set;}
    public DbSet<Evento> Eventos {get; set;}

    public SupersContext(DbContextOptions<SupersContext> options) :base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        
        List<Super> supersInit = new List<Super>();
        supersInit.Add(new Super() {
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
            nombre = "Gamora",
            edad = 22,
            rol_super = Rol.Heroe,
            relaciones = "Hijo de thanos",
            origen = "Titán"
        });
        supersInit.Add(new Super() {
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4eg"),
            nombre = "Thanos",
            edad = 40,
            rol_super = Rol.Villano,
            relaciones = "Hijo de eternos",
            origen = "Titán"
        });
        modelBuilder.Entity<Super>(super => {
            super.ToTable("super");
            super.HasKey(p=>p.super_id);
            super.Property(p=> p.nombre).IsRequired();
            super.Property(p=> p.edad).IsRequired();
            super.Property(p=> p.rol_super).IsRequired();
            super.Property(p=> p.relaciones);
            super.Property(p=> p.origen);

            super.HasData(supersInit);
        });

        List<Rasgo> rasgosInit = new List<Rasgo>();
        rasgosInit.Add(new Rasgo() {
            rasgo_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"),
            titulo = "Superfuerza",
            tipo_rasgo = TipoRasgo.Habilidad
        });
        modelBuilder.Entity<Rasgo>(rasgo => {
            rasgo.ToTable("rasgo");
            rasgo.HasKey(p=>p.rasgo_id);
            rasgo.Property(p=> p.titulo).IsRequired();
            rasgo.Property(p=> p.tipo_rasgo).IsRequired();

            rasgo.HasData(rasgosInit);
        });

        List<RasgoSuper> rasgosSupersInit = new List<RasgoSuper>();
        rasgosSupersInit.Add(new RasgoSuper() {
            rasgo_super_id = Guid.NewGuid(),
            rasgo_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"),
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef") 
        });
        modelBuilder.Entity<RasgoSuper>(rasgosSuper => {
            rasgosSuper.ToTable("rasgo_super");
            rasgosSuper.HasKey(p=>p.rasgo_super_id);
            rasgosSuper.HasOne(p=>p.Rasgo).WithMany(p=>p.RasgosSuper).HasForeignKey(p=>p.super_id);
            rasgosSuper.HasOne(p=>p.Super).WithMany(p=>p.RasgosSuper).HasForeignKey(p=>p.super_id);

            rasgosSuper.HasData(rasgosSupersInit);
        });

        List<Enfrentamiento> enfrentamientoInit = new List<Enfrentamiento>();
        enfrentamientoInit.Add(new Enfrentamiento() {
            enfrentamiento_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4eh"),
            titulo = "Pelea de Wakanda",
            fecha = DateTime.Now
        });
        modelBuilder.Entity<Enfrentamiento>(enfrentamiento => {
            enfrentamiento.ToTable("enfrentamiento");
            enfrentamiento.HasKey(p=>p.enfrentamiento_id);
            enfrentamiento.Property(p=> p.titulo).IsRequired();
            enfrentamiento.Property(p=> p.fecha).IsRequired();

            enfrentamiento.HasData(enfrentamientoInit);
        });

        List<Pelea> peleaInit = new List<Pelea>();
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
            enfrentamiento_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4eh"),
            gana = true
        });
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4eg"),
            enfrentamiento_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4eh"),
            gana = false
        });
        modelBuilder.Entity<Pelea>(pelea => {
            pelea.ToTable("pelea");
            pelea.HasKey(p=>p.pelea_id);
            pelea.HasOne(p=>p.Super).WithMany(p=>p.Peleas).HasForeignKey(p=>p.super_id);
            pelea.HasOne(p=>p.Enfrentamiento).WithMany(p=>p.Peleas).HasForeignKey(p=>p.enfrentamiento_id);
            pelea.Property(p=> p.gana).IsRequired();

            pelea.HasData(rasgosInit);
        });

        List<Patrocinador> patrocinadorInit = new List<Patrocinador>();
        patrocinadorInit.Add(new Patrocinador() {});
        modelBuilder.Entity<Patrocinador>(patrocinador => {
            patrocinador.ToTable("patrocinador");
            patrocinador.HasKey(p=>p.patrocinador_id);
            patrocinador.HasOne(p=>p.Super).WithMany(p=>p.Patrocinadores).HasForeignKey(p=>p.super_id);
            patrocinador.Property(p=> p.nombre).IsRequired();
            patrocinador.Property(p=> p.monto).IsRequired();
            patrocinador.Property(p=> p.origen_monto).IsRequired();

            patrocinador.HasData(patrocinadorInit);
        });

        List<Evento> eventoInit = new List<Evento>();
        eventoInit.Add(new Evento() {});
        modelBuilder.Entity<Evento>(evento => {
            evento.ToTable("evento");
            evento.HasKey(p=>p.evento_id);
            evento.HasOne(p=>p.Super).WithMany(p=>p.Eventos).HasForeignKey(p=>p.super_id);
            evento.Property(p=> p.titulo).IsRequired();
            evento.Property(p=> p.inicio).IsRequired();
            evento.Property(p=> p.fin).IsRequired();
            evento.Property(p=> p.descripcion);
            evento.Property(p=> p.lugar);

            evento.HasData(eventoInit);
        });
    }

}