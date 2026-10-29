using System.ComponentModel.DataAnnotations;
namespace Tarea4.Models;
public class Estudiante
    {
        public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    public string Correo { get; set; } = string.Empty;

    public int CursoId { get; set; }

    public Curso? Curso { get; set; }
    }
