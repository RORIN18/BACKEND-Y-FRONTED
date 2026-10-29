using Microsoft.EntityFrameworkCore;
using Tarea4.Core;
using Tarea4.Dtos;
using Tarea4.Models;

namespace Tarea4.Services;

public class EstudianteService : IEstudianteService
{
    private readonly EscuelaContext _context;

    public EstudianteService(EscuelaContext context)
    {
        _context = context;
    }

    public async Task<List<EstudianteRespuestaDto>> ObtenerTodos()
    {
        return await _context.Estudiantes
            .Select(e => new EstudianteRespuestaDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Correo = e.Correo,
                CursoId = e.CursoId,
                NombreCurso = e.Curso!.Nombre
            })
            .ToListAsync();
    }

    public async Task<EstudianteRespuestaDto?> ObtenerPorId(int id)
    {
        return await _context.Estudiantes
            .Where(e => e.Id == id)
            .Select(e => new EstudianteRespuestaDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Correo = e.Correo,
                CursoId = e.CursoId,
                NombreCurso = e.Curso!.Nombre
            })
            .FirstOrDefaultAsync();
    }

    public async Task<EstudianteRespuestaDto?> Crear(CrearEstudianteDto dto)
    {
        bool cursoExiste = await _context.Cursos
            .AnyAsync(c => c.Id == dto.CursoId);

        if (!cursoExiste)
            return null;

        Estudiante estudiante = new Estudiante
        {
            Nombre = dto.Nombre,
            Correo = dto.Correo,
            CursoId = dto.CursoId
        };

        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        return await ObtenerPorId(estudiante.Id);
    }

    public async Task<bool> Actualizar(int id, ActualizarEstudianteDto dto)
    {
        Estudiante? estudiante = await _context.Estudiantes
            .FindAsync(id);

        if (estudiante == null)
            return false;

        bool cursoExiste = await _context.Cursos
            .AnyAsync(c => c.Id == dto.CursoId);

        if (!cursoExiste)
            return false;

        estudiante.Nombre = dto.Nombre;
        estudiante.Correo = dto.Correo;
        estudiante.CursoId = dto.CursoId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        Estudiante? estudiante = await _context.Estudiantes
            .FindAsync(id);

        if (estudiante == null)
            return false;

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();

        return true;
    }
}