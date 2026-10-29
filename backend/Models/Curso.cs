using System.ComponentModel.DataAnnotations;
namespace Tarea4.Models;
public class Curso
{
     public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del curso es obligatorio")]
    [StringLength(100, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Descripcion { get; set; }

    public ICollection<Estudiante>? Estudiantes { get; set; }
    
}