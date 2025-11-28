/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

namespace TempoControl.Domain

{
public class Empleado

{
public int Id { get; set; }
public string NombreCompleto { get; set; } = null!;
public string Departamento { get; set; } = null!;
public string Posicion { get; set; } = null!;
public bool Activo { get; set; } = true;
    
}

}