using Tarea4.Dtos;

namespace Tarea4.Services;

public interface IEstudianteService
{
    Task<List<EstudianteRespuestaDto>> ObtenerTodos();
    Task<EstudianteRespuestaDto?> ObtenerPorId(int id);
    Task<EstudianteRespuestaDto?> Crear(CrearEstudianteDto dto);
    Task<bool> Actualizar(int id, ActualizarEstudianteDto dto);
    Task<bool> Eliminar(int id);
}