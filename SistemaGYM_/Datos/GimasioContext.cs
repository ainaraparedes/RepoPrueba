using Microsoft.EntityFrameworkCore;
using SistemaGYM.Entidades;

namespace SistemaGYM.Datos;

public class GimnasioContext(DbContextOptions<GimnasioContext> options) : DbContext(options)
{
    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Alimentacion> Alimentaciones { get; set; }
    public DbSet<Anuncio> Anuncios { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Rutina> Rutinas { get; set; }
    public DbSet<Suscripcion> Suscripciones { get; set; }
    public DbSet<AlumnoSuscripcion> AlumnoSuscripciones { get; set; }
    public DbSet<ActividadAlumno> ActividadesAlumno { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("TipoUsuario")
            .HasValue<Alumno>("Alumno")
            .HasValue<Profesor>("Profesor");

        modelBuilder.Entity<Pago>()
            .Property(p => p.MetodoPago)
            .HasConversion<string>();

        var admin = new Administrador { Id = 1, Usuario = "Admin123" };
        admin.SetContrasenia("Contraseña123");

        modelBuilder.Entity<Administrador>().HasData(admin);
    }
}