using Tarea4.Dtos;

namespace Tarea4.Services;

public interface ICursoService
{
    Task<List<CursoRespuestaDto>> ObtenerTodos();
    Task<CursoRespuestaDto?> ObtenerPorId(int id);
    Task<CursoRespuestaDto> Crear(CrearCursoDto dto);
    Task<bool> Actualizar(int id, ActualizarCursoDto dto);
    Task<bool> Eliminar(int id);
}