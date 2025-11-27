/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using System;
using TempoControl.Data;
using TempoControl.Domain;

namespace TempoControl.Business

{
public class FichajeService
    
{
        
private readonly IRegistroFichajeRepository _regRepo;
private readonly IEmpleadoRepository _empRepo;

public FichajeService(IRegistroFichajeRepository regRepo, IEmpleadoRepository empRepo)
        
{
            
_regRepo = regRepo;
_empRepo = empRepo;
        
}
public string RegistrarEntrada(int empleadoId)
        
{
            
var empleado = _empRepo.GetById(empleadoId);
if (empleado == null) return "Empleado no encontrado.";
if (!empleado.Activo) return "Empleado inactivo.";

// Si hay registro abierto, no permitir nueva entrada hasta que salga
var open = _regRepo.GetOpenRegistroByEmpleado(empleadoId);
if (open != null) return "Ya existe una entrada sin salida registrada.";

var reg = new RegistroFichaje
            
{
                
EmpleadoId = empleadoId,
FechaHoraEntrada = DateTime.Now,
FechaHoraSalida = null
            
};
            
_regRepo.Create(reg);
return $"Entrada registrada para {empleado.NombreCompleto} a las {reg.FechaHoraEntrada}.";
        
}
public string RegistrarSalida(int empleadoId)
        
{
            
var empleado = _empRepo.GetById(empleadoId);
if (empleado == null) return "Empleado no encontrado.";

var open = _regRepo.GetOpenRegistroByEmpleado(empleadoId);
if (open == null) return "No hay una entrada abierta para este empleado.";

open.FechaHoraSalida = DateTime.Now;
_regRepo.Update(open);

var total = open.FechaHoraSalida.Value - open.FechaHoraEntrada;
return $"Salida registrada para {empleado.NombreCompleto} a las {open.FechaHoraSalida}. Duración: {total.TotalHours:F2} horas.";
        
}
    
}

}