/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using TempoControl.Domain;
using System.Collections.Generic;

namespace TempoControl.Data

{
public interface IEmpleadoRepository
    
{
IEnumerable<Empleado> GetAll();
Empleado? GetById(int id);
Empleado? GetByName(string nombre);
Empleado Create(Empleado empleado);
void Update(Empleado empleado);
void Deactivate(int id);
    
}

}