using Microsoft.AspNetCore.Mvc;
using Tarea4.Dtos;
using Tarea4.Services;

namespace Tarea4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursosController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CursoRespuestaDto>>> ObtenerTodos()
    {
        return Ok(await _cursoService.ObtenerTodos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CursoRespuestaDto>> ObtenerPorId(int id)
    {
        var curso = await _cursoService.ObtenerPorId(id);

        if (curso == null)
            return NotFound(new { mensaje = "Curso no encontrado" });

        return Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult<CursoRespuestaDto>> Crear(CrearCursoDto dto)
    {
        var curso = await _cursoService.Crear(dto);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = curso.Id },
            curso
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarCursoDto dto)
    {
        bool actualizado = await _cursoService.Actualizar(id, dto);

        if (!actualizado)
            return NotFound(new { mensaje = "Curso no encontrado" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool eliminado = await _cursoService.Eliminar(id);

        if (!eliminado)
            return BadRequest(new
            {
                mensaje = "Curso no encontrado o tiene estudiantes asociados"
            });

        return NoContent();
    }
}