/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using Microsoft.EntityFrameworkCore;
using TempoControl.Domain;
using System.Collections.Generic;
using System.Linq;

namespace TempoControl.Data

{
public class EmpleadoRepository : IEmpleadoRepository
    
{

private readonly TempoContext _db;
public EmpleadoRepository(TempoContext db)
        
{
_db = db;
        
}
public Empleado Create(Empleado empleado)
        
{
            
_db.Empleados.Add(empleado);
_db.SaveChanges();
            
return empleado;
        
}
public void Deactivate(int id)
        
{
  
var e = _db.Empleados.Find(id);
if (e != null)
            
{

e.Activo = false;
_db.SaveChanges();
            
}
        
}

public IEnumerable<Empleado> GetAll()
        
{
            
return _db.Empleados.AsNoTracking().ToList();
        
}
public Empleado? GetById(int id)
        
{
            
return _db.Empleados.Find(id);
        
}
public Empleado? GetByName(string nombre)
        
{
            
return _db.Empleados
.AsNoTracking()
.FirstOrDefault(e => e.NombreCompleto.ToLower() == nombre.ToLower());
        
}
public void Update(Empleado empleado)
        
{
            
var existing = _db.Empleados.Find(empleado.Id);
if (existing != null)
            
{
                
existing.NombreCompleto = empleado.NombreCompleto;
existing.Departamento = empleado.Departamento;
existing.Posicion = empleado.Posicion;
existing.Activo = empleado.Activo;
_db.SaveChanges();
            
}
        
}
    
}

}