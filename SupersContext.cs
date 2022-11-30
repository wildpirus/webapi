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

    public SupersContext(DbContextOptions<SupersContext> options) :base(options) { 
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        
        List<Super> supersInit = new List<Super>();
        supersInit.Add(new Super() {
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
            nombre = "Gamora",
            edad = 22,
            image_link = "https://www.pngplay.com/wp-content/uploads/12/Gamora-Transparent-PNG.png",
            rol_super = "Heroe",
            relaciones = "Hijo de thanos",
            origen = "Titán"
        });
        supersInit.Add(new Super() {
            super_id = Guid.Parse("37c2b061-9924-4595-8deb-f1c7f02c9bca"),
            nombre = "Thanos",
            edad = 40,
            image_link = "https://static.wikia.nocookie.net/marveldatabase/images/b/bc/Thanos_%28Earth-199999%29_from_Avengers_Infinity_War_-_The_Official_Movie_Special_001.jpg/revision/latest?cb=20180501065444",
            rol_super = "Villano",
            relaciones = "Hijo de eternos",
            origen = "Titán"
        });
        supersInit.Add(new Super() {
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554"),
            nombre = "Thor Odinson",
            edad = 18,
            image_link = "https://i.blogs.es/1c1b9b/chris-hemsworth-thor/450_1000.jpeg",
            rol_super = "Heroe",
            relaciones = "Hijo de Odin",
            origen = "Asgard"
        });
        supersInit.Add(new Super() {
            super_id = Guid.Parse("a522fa66-5552-4896-ad88-5f2557b3a6cc"),
            nombre = "Loki",
            edad = 15,
            image_link = "https://phantom-marca.unidadeditorial.es/456c64c083e34bd2601f7bb200f3447b/resize/1320/f/jpg/assets/multimedia/imagenes/2021/06/07/16230600360752.jpg",
            rol_super = "Villano",
            relaciones = "Hijo de eternos",
            origen = "Titán"
        });
        modelBuilder.Entity<Super>(super => {
            super.ToTable("super");
            super.HasKey(p=>p.super_id);
            super.Property(p=> p.nombre).IsRequired();
            super.Property(p=> p.edad).IsRequired();
            super.Property(p=> p.rol_super).IsRequired();
            super.Property(p=> p.image_link).IsRequired();
            super.Property(p=> p.relaciones);
            super.Property(p=> p.origen);

            super.HasData(supersInit);
        });

        List<Rasgo> rasgosInit = new List<Rasgo>();
        rasgosInit.Add(new Rasgo() {
            rasgo_id = Guid.Parse("d4687a08-ba31-48b3-91ea-af2f56e80daf"),
            titulo = "Superfuerza",
            tipo_rasgo = "Habilidad"
        });
        rasgosInit.Add(new Rasgo() {
            rasgo_id = Guid.Parse("60cb6ca1-ee8e-4d32-abbb-80c031424743"),
            titulo = "Velocidad",
            tipo_rasgo = "Habilidad"
        });
        rasgosInit.Add(new Rasgo() {
            rasgo_id = Guid.Parse("99b18b03-4452-42e7-93b5-4afb733d4865"),
            titulo = "Tiempo",
            tipo_rasgo = "Habilidad"
        });
        rasgosInit.Add(new Rasgo() {
            rasgo_id = Guid.Parse("45607d43-0d3d-4637-897d-8782e712df97"),
            titulo = "Ilusiones",
            tipo_rasgo = "Habilidad"
        });
        rasgosInit.Add(new Rasgo() {
            rasgo_id = Guid.Parse("408e38ad-1fd5-4dc9-a534-3ad44e7017b5"),
            titulo = "Sylvie",
            tipo_rasgo = "Debilidad"
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
            rasgo_id = Guid.Parse("d4687a08-ba31-48b3-91ea-af2f56e80daf"),
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef") 
        });
        rasgosSupersInit.Add(new RasgoSuper() {
            rasgo_super_id = Guid.NewGuid(),
            rasgo_id = Guid.Parse("60cb6ca1-ee8e-4d32-abbb-80c031424743"),
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef") 
        });
        rasgosSupersInit.Add(new RasgoSuper() {
            rasgo_super_id = Guid.NewGuid(),
            rasgo_id = Guid.Parse("99b18b03-4452-42e7-93b5-4afb733d4865"),
            super_id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef") 
        });
        rasgosSupersInit.Add(new RasgoSuper() {
            rasgo_super_id = Guid.NewGuid(),
            rasgo_id = Guid.Parse("45607d43-0d3d-4637-897d-8782e712df97"),
            super_id = Guid.Parse("a522fa66-5552-4896-ad88-5f2557b3a6cc") 
        });
        rasgosSupersInit.Add(new RasgoSuper() {
            rasgo_super_id = Guid.NewGuid(),
            rasgo_id = Guid.Parse("408e38ad-1fd5-4dc9-a534-3ad44e7017b5"),
            super_id = Guid.Parse("a522fa66-5552-4896-ad88-5f2557b3a6cc") 
        });
        rasgosSupersInit.Add(new RasgoSuper() {
            rasgo_super_id = Guid.NewGuid(),
            rasgo_id = Guid.Parse("d4687a08-ba31-48b3-91ea-af2f56e80daf"),
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554") 
        });
        modelBuilder.Entity<RasgoSuper>(rasgosSuper => {
            rasgosSuper.ToTable("rasgo_super");
            rasgosSuper.HasKey(p=>p.rasgo_super_id);
            rasgosSuper.HasOne(p=>p.Rasgo).WithMany(p=>p.RasgosSuper).HasForeignKey(p=>p.rasgo_id);
            rasgosSuper.HasOne(p=>p.Super).WithMany(p=>p.RasgosSuper).HasForeignKey(p=>p.super_id);

            rasgosSuper.HasData(rasgosSupersInit);
        });

        List<Enfrentamiento> enfrentamientoInit = new List<Enfrentamiento>();
        enfrentamientoInit.Add(new Enfrentamiento() {
            enfrentamiento_id = Guid.Parse("1509a475-1bae-422d-b084-546bd98cebe7"),
            titulo = "Pelea de Wakanda",
            fecha = DateTime.Now
        });
        enfrentamientoInit.Add(new Enfrentamiento() {
            enfrentamiento_id = Guid.Parse("f1f65c20-6939-47ae-a3ed-bf87f7d13c57"),
            titulo = "Pelea de Asgard",
            fecha = DateTime.Now
        });
        enfrentamientoInit.Add(new Enfrentamiento() {
            enfrentamiento_id = Guid.Parse("fe12ff38-63f0-4c02-9da2-f5d8dd5ace43"),
            titulo = "Pelea de Sakarr",
            fecha = DateTime.Now
        });
        enfrentamientoInit.Add(new Enfrentamiento() {
            enfrentamiento_id = Guid.Parse("da4b09d8-8196-4702-b3a4-34d9135be5b8"),
            titulo = "Pelea de Tierra",
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
            enfrentamiento_id = Guid.Parse("1509a475-1bae-422d-b084-546bd98cebe7"),
            gana = true
        });
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("37c2b061-9924-4595-8deb-f1c7f02c9bca"),
            enfrentamiento_id = Guid.Parse("1509a475-1bae-422d-b084-546bd98cebe7"),
            gana = false
        });
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554"),
            enfrentamiento_id = Guid.Parse("f1f65c20-6939-47ae-a3ed-bf87f7d13c57"),
            gana = true
        });
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("a522fa66-5552-4896-ad88-5f2557b3a6cc"),
            enfrentamiento_id = Guid.Parse("f1f65c20-6939-47ae-a3ed-bf87f7d13c57"),
            gana = false
        });
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554"),
            enfrentamiento_id = Guid.Parse("da4b09d8-8196-4702-b3a4-34d9135be5b8"),
            gana = true
        });
        peleaInit.Add(new Pelea() {
            pelea_id = Guid.NewGuid(),
            super_id = Guid.Parse("a522fa66-5552-4896-ad88-5f2557b3a6cc"),
            enfrentamiento_id = Guid.Parse("da4b09d8-8196-4702-b3a4-34d9135be5b8"),
            gana = false
        });
        modelBuilder.Entity<Pelea>(pelea => {
            pelea.ToTable("pelea");
            pelea.HasKey(p=>p.pelea_id);
            pelea.HasOne(p=>p.Super).WithMany(p=>p.Peleas).HasForeignKey(p=>p.super_id);
            pelea.HasOne(p=>p.Enfrentamiento).WithMany(p=>p.Peleas).HasForeignKey(p=>p.enfrentamiento_id);
            pelea.Property(p=> p.gana).IsRequired();

            pelea.HasData(peleaInit);
        });

        List<Patrocinador> patrocinadorInit = new List<Patrocinador>();
        patrocinadorInit.Add(new Patrocinador() {
            patrocinador_id = Guid.NewGuid(),
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554"),
            nombre = "Lady Siff",
            monto = 100000000,
            origen_monto = "Alcohol ilegal Asgardiano"
        });
        patrocinadorInit.Add(new Patrocinador() {
            patrocinador_id = Guid.NewGuid(),
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554"),
            nombre = "Odin",
            monto = 999999999,
            origen_monto = "Magia Oscura"
        });
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
        eventoInit.Add(new Evento() {
            evento_id = Guid.NewGuid(),
            super_id = Guid.Parse("c20555e5-6fe7-40a4-9642-8f83c9e5a554"),
            titulo = "Estudien",
            inicio = new DateTime(),
            fin = new DateTime(),
            descripcion = "Clase de danza",
            lugar = "Uninorte"
        });
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