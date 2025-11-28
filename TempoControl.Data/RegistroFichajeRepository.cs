/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using Microsoft.EntityFrameworkCore;
using TempoControl.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TempoControl.Data

{
public class RegistroFichajeRepository : IRegistroFichajeRepository
    
{
        
private readonly TempoContext _db;
public RegistroFichajeRepository(TempoContext db)
        
{
            
_db = db;
        
}
public RegistroFichaje Create(RegistroFichaje registro)
        
{

_db.RegistrosFichaje.Add(registro);
_db.SaveChanges();
return registro;
        
}
public RegistroFichaje? GetOpenRegistroByEmpleado(int empleadoId)
        
{
            
return _db.RegistrosFichaje
.Where(r => r.EmpleadoId == empleadoId && r.FechaHoraSalida == null)
.OrderByDescending(r => r.FechaHoraEntrada)
.FirstOrDefault();
        
}
public IEnumerable<RegistroFichaje> GetByEmpleadoAndMonth(int empleadoId, int month, int year)
        
{
            
return _db.RegistrosFichaje
.Where(r => r.EmpleadoId == empleadoId &&
r.FechaHoraEntrada.Year == year &&
r.FechaHoraEntrada.Month == month &&
r.FechaHoraSalida != null)
.AsNoTracking()
.ToList();
        
}
public void Update(RegistroFichaje registro)
        
{
            
_db.RegistrosFichaje.Update(registro);
_db.SaveChanges();
        
}
    
}

}