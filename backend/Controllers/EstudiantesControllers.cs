using Microsoft.AspNetCore.Mvc;
using Tarea4.Dtos;
using Tarea4.Services;

namespace Tarea4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteService _estudianteService;

    public EstudiantesController(IEstudianteService estudianteService)
    {
        _estudianteService = estudianteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EstudianteRespuestaDto>>> ObtenerTodos()
    {
        return Ok(await _estudianteService.ObtenerTodos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstudianteRespuestaDto>> ObtenerPorId(int id)
    {
        var estudiante = await _estudianteService.ObtenerPorId(id);

        if (estudiante == null)
            return NotFound(new { mensaje = "Estudiante no encontrado" });

        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<ActionResult<EstudianteRespuestaDto>> Crear(CrearEstudianteDto dto)
    {
        var estudiante = await _estudianteService.Crear(dto);

        if (estudiante == null)
            return BadRequest(new { mensaje = "El curso indicado no existe" });

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = estudiante.Id },
            estudiante
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarEstudianteDto dto)
    {
        bool actualizado = await _estudianteService.Actualizar(id, dto);

        if (!actualizado)
            return BadRequest(new
            {
                mensaje = "Estudiante no encontrado o el curso indicado no existe"
            });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool eliminado = await _estudianteService.Eliminar(id);

        if (!eliminado)
            return NotFound(new { mensaje = "Estudiante no encontrado" });

        return NoContent();
    }
}