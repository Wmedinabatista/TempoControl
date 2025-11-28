/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using Microsoft.EntityFrameworkCore;
using TempoControl.Domain;

namespace TempoControl.Data

{
public class TempoContext : DbContext
    
{
public DbSet<Empleado> Empleados { get; set; } = null!;
public DbSet<RegistroFichaje> RegistrosFichaje { get; set; } = null!;

public TempoContext()
        
{
        
}

public TempoContext(DbContextOptions<TempoContext> options) : base(options)
        
{
        
}

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
{
            
if (!optionsBuilder.IsConfigured)
            
{
                
// Archivo DB local (se creará en el directorio de ejecución)
optionsBuilder.UseSqlite("Data Source=tempo.db");
            
}
        
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
        
{
            
modelBuilder.Entity<Empleado>(eb =>
           
{
                
eb.HasKey(e => e.Id);
eb.Property(e => e.NombreCompleto).IsRequired();
eb.Property(e => e.Departamento).IsRequired();
eb.Property(e => e.Posicion).IsRequired();
eb.Property(e => e.Activo).HasDefaultValue(true);
            
});

modelBuilder.Entity<RegistroFichaje>(rb =>
            
{
                
rb.HasKey(r => r.Id);
rb.HasOne(r => r.Empleado)
.WithMany()
.HasForeignKey(r => r.EmpleadoId)
.OnDelete(DeleteBehavior.Restrict);
rb.Property(r => r.FechaHoraEntrada).IsRequired();
            
});
        
}
    
}

}