/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using System.Collections.Generic;
using TempoControl.Data;
using TempoControl.Domain;

namespace TempoControl.Business

{
public class EmpleadoService
    
{
        
private readonly IEmpleadoRepository _repo;
public EmpleadoService(IEmpleadoRepository repo)
        
{
            
_repo = repo;
        
}
public IEnumerable<Empleado> GetAll() => _repo.GetAll();

public Empleado? GetById(int id) => _repo.GetById(id);

public Empleado Create(string nombre, string departamento, string posicion)
        
{
            
var e = new Empleado
            
{
                
NombreCompleto = nombre,
Departamento = departamento,
Posicion = posicion,
Activo = true
            
};
 
return _repo.Create(e);
        
}
public void Update(Empleado empleado) => _repo.Update(empleado);

public void Deactivate(int id) => _repo.Deactivate(id);
    
}

}