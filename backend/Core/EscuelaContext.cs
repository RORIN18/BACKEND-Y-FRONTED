using Microsoft.EntityFrameworkCore;
using Tarea4.Models;

namespace Tarea4.Core;

public class EscuelaContext : DbContext
{
    public EscuelaContext(DbContextOptions<EscuelaContext> options)
        : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Curso> Cursos { get; set; }
}