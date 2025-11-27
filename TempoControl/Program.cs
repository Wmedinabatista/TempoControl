/*Autor: Wilmy Medina Batista.
Matricula: 100681393.
Fecha: 23/11/2025.
Diseñar e implementar un sistema de software (en C#) que gestione el fichaje (ponchado) de 
empleados, aplicando correctamente los principios de persistencia de datos mediante un patrón de 
arquitectura desacoplado (Patrón Repositorio) y una base de datos relacional.*/

using System;
using TempoControl.Data;
using TempoControl.Business;
using TempoControl.Domain;
using Microsoft.EntityFrameworkCore;

class Program

{
static void Main()
    
{
        
// Inicializar DbContext y Repositorios
using var db = new TempoContext();
// Crea la BD si no existe
db.Database.EnsureCreated();

var empleadoRepo = new EmpleadoRepository(db);
var registroRepo = new RegistroFichajeRepository(db);

var empleadoService = new EmpleadoService(empleadoRepo);
var fichajeService = new FichajeService(registroRepo, empleadoRepo);
var reporteService = new ReporteService(empleadoRepo, registroRepo);

bool salir = false;
while (!salir)
        
{
            
Console.Clear();
Console.WriteLine("=== TempoControl ===");
Console.WriteLine("1. Gestión de empleados (CRUD)");
Console.WriteLine("2. Registrar entrada");
Console.WriteLine("3. Registrar salida");
Console.WriteLine("4. Generar reporte mensual");
Console.WriteLine("5. Salir");
Console.Write("Elija una opción: ");
var opt = Console.ReadLine();
switch (opt)
            
{
                
case "1": GestionEmpleados(empleadoService); break;
case "2": RegistrarEntrada(fichajeService); break;
case "3": RegistrarSalida(fichajeService); break;
case "4": GenerarReporte(reporteService); break;
case "5": salir = true; break;
default:
Console.WriteLine("Opción inválida. Presione Enter para continuar...");
Console.ReadLine();
break;
            
}
        
}
    
}

static void GestionEmpleados(EmpleadoService service)
    
{
        
bool volver = false;
while (!volver)
        
{
            
Console.Clear();
Console.WriteLine("=== Gestión de Empleados ===");
Console.WriteLine("1. Listar empleados");
Console.WriteLine("2. Crear empleado");
Console.WriteLine("3. Actualizar empleado");
Console.WriteLine("4. Desactivar empleado");
Console.WriteLine("5. Volver");
Console.Write("Opción: ");
var op = Console.ReadLine();
switch (op)
            
{
                
case "1":
var all = service.GetAll();
Console.WriteLine("ID | Nombre | Departamento | Posición | Activo");
foreach (var e in all)
                    
{
                        
Console.WriteLine($"{e.Id} | {e.NombreCompleto} | {e.Departamento} | {e.Posicion} | {e.Activo}");
                    
}
                    
Console.WriteLine("Presione Enter para continuar...");
Console.ReadLine();
break;
case "2":
Console.Write("Nombre completo: ");
var nombre = Console.ReadLine() ?? "";
Console.Write("Departamento: ");
var dep = Console.ReadLine() ?? "";
Console.Write("Posición: ");
var pos = Console.ReadLine() ?? "";
var nuevo = service.Create(nombre, dep, pos);
Console.WriteLine($"Empleado creado con Id {nuevo.Id}");
Console.WriteLine("Enter...");
Console.ReadLine();
break;
case "3":
Console.Write("ID a actualizar: ");
if (int.TryParse(Console.ReadLine(), out int idUpd))
             
{
                        
var e = service.GetById(idUpd);
if (e == null) { Console.WriteLine("Empleado no encontrado."); Console.ReadLine(); break; }
Console.Write($"Nombre ({e.NombreCompleto}): ");
var nn = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(nn)) e.NombreCompleto = nn;
Console.Write($"Departamento ({e.Departamento}): ");
var nd = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(nd)) e.Departamento = nd;
Console.Write($"Posición ({e.Posicion}): ");
var np = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(np)) e.Posicion = np;
Console.Write($"Activo ({e.Activo}) (true/false): ");
var na = Console.ReadLine();
if (bool.TryParse(na, out bool b)) e.Activo = b;
service.Update(e);
Console.WriteLine("Empleado actualizado.");

}
                    
else Console.WriteLine("ID inválido.");
Console.ReadLine();
break;
case "4":
Console.Write("ID a desactivar: ");
if (int.TryParse(Console.ReadLine(), out int idDel))
                    
{
                        
service.Deactivate(idDel);
Console.WriteLine("Empleado desactivado (si existía).");
                    
}
                    
else Console.WriteLine("ID inválido.");
Console.ReadLine();
break;
case "5":
volver = true;
break;
default:
Console.WriteLine("Opción inválida.");
Console.ReadLine();
break;

}
        
}
    
}

static void RegistrarEntrada(FichajeService service)
    
{
        
Console.Clear();
Console.Write("ID del empleado: ");
if (int.TryParse(Console.ReadLine(), out int id))
        
{
            
var r = service.RegistrarEntrada(id);
Console.WriteLine(r);
        
}
        
else Console.WriteLine("ID inválido.");
Console.WriteLine("Enter...");
Console.ReadLine();
    
}
static void RegistrarSalida(FichajeService service)
    
{
        
Console.Clear();
Console.Write("ID del empleado: ");
if (int.TryParse(Console.ReadLine(), out int id))
        
{
            
var r = service.RegistrarSalida(id);
Console.WriteLine(r);
        
}
        
else Console.WriteLine("ID inválido.");
Console.WriteLine("Enter...");
Console.ReadLine();
    
}

static void GenerarReporte(ReporteService service)
    
{
        
Console.Clear();
Console.Write("Mes (1-12): ");
if (!int.TryParse(Console.ReadLine(), out int mes) || mes < 1 || mes > 12)
        
{
     
Console.WriteLine("Mes inválido."); Console.ReadLine(); return;
        
}
        
Console.Write("Año (ej. 2025): ");
if (!int.TryParse(Console.ReadLine(), out int año) || año < 2000)
       
{
            
Console.WriteLine("Año inválido."); Console.ReadLine(); return;
        
}

var reporte = service.GenerarReporte(mes, año);
Console.WriteLine($"Reporte {mes}/{año}");
Console.WriteLine("Nombre | Días trabajados | Horas totales");
foreach (var r in reporte)
        
{
            
Console.WriteLine($"{r.Nombre} | {r.DiasTrabajados} | {r.HorasTotales:F2}");
        
}
        
Console.WriteLine("Enter...");
Console.ReadLine();
    
}

}