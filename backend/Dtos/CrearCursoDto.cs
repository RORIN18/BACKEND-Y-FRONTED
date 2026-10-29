using System.ComponentModel.DataAnnotations;

namespace Tarea4.Dtos;

public class CrearCursoDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Descripcion { get; set; }
}