using Microsoft.EntityFrameworkCore;
using Tarea4.Core;
using Tarea4.Dtos;
using Tarea4.Models;

namespace Tarea4.Services;

public class CursoService : ICursoService
{
    private readonly EscuelaContext _context;

    public CursoService(EscuelaContext context)
    {
        _context = context;
    }

    public async Task<List<CursoRespuestaDto>> ObtenerTodos()
    {
        return await _context.Cursos
            .Select(c => new CursoRespuestaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            })
            .ToListAsync();
    }

    public async Task<CursoRespuestaDto?> ObtenerPorId(int id)
    {
        return await _context.Cursos
            .Where(c => c.Id == id)
            .Select(c => new CursoRespuestaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CursoRespuestaDto> Crear(CrearCursoDto dto)
    {
        Curso curso = new Curso
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion
        };

        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();

        return new CursoRespuestaDto
        {
            Id = curso.Id,
            Nombre = curso.Nombre,
            Descripcion = curso.Descripcion
        };
    }

    public async Task<bool> Actualizar(int id, ActualizarCursoDto dto)
    {
        Curso? curso = await _context.Cursos.FindAsync(id);

        if (curso == null)
            return false;

        curso.Nombre = dto.Nombre;
        curso.Descripcion = dto.Descripcion;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        Curso? curso = await _context.Cursos
            .Include(c => c.Estudiantes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null || curso.Estudiantes.Any())
            return false;

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();

        return true;
    }
}