/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

namespace TempoControl.Domain

{
public class RegistroFichaje
    
{
public int Id { get; set; }
public int EmpleadoId { get; set; }
public Empleado? Empleado { get; set; }

// Hora entrada obligatoria
public DateTime FechaHoraEntrada { get; set; }

// Hora salida puede ser nula hasta que el empleado salga
public DateTime? FechaHoraSalida { get; set; }
    
}

}