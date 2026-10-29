import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { AutoFocusDirective } from '../../shared/directives/auto-focus.directive';
import { InicialesPipe } from '../../shared/pipes/iniciales.pipe';
import { CrearEstudiante, Curso, Estudiante } from '../../models/escuela.models';
import { CursosService } from '../../services/cursos.service';
import { EstudiantesService } from '../../services/estudiantes.service';
@Component({ selector: 'app-estudiantes-page', standalone: true, imports: [FormsModule, AutoFocusDirective, InicialesPipe], templateUrl: './estudiantes.page.html' })
export class EstudiantesPage implements OnInit {
  private readonly service = inject(EstudiantesService);
  private readonly cursosService = inject(CursosService);
  estudiantes: Estudiante[] = [];
  cursos: Curso[] = [];
  filtro = '';
  cargando = true;
  guardando = false;
  editandoId: number | null = null;
  aviso = '';
  tipoAviso: 'error' | 'success' = 'success';
  form = { nombre: '', correo: '', cursoId: 0 };
  get estudiantesFiltrados(): Estudiante[] {
    const q = this.filtro.trim().toLocaleLowerCase();
    return this.estudiantes.filter(e => `${e.nombre} ${e.correo} ${e.nombreCurso}`.toLocaleLowerCase().includes(q));
  }
  ngOnInit(): void { this.cargarCursos(); this.cargar(); }
  cargar(): void {
    this.cargando = true;
    this.service.listar().subscribe({ next: data => { this.estudiantes = data; this.cargando = false; }, error: e => { this.mostrarError(e); this.cargando = false; } });
  }
  cargarCursos(): void { this.cursosService.listar().subscribe({ next: data => this.cursos = data, error: e => this.mostrarError(e) }); }
  editar(estudiante: Estudiante): void { this.editandoId = estudiante.id; this.form = { nombre: estudiante.nombre, correo: estudiante.correo, cursoId: estudiante.cursoId }; this.aviso = ''; }
  cancelar(): void { this.editandoId = null; this.form = { nombre: '', correo: '', cursoId: 0 }; }
  guardar(): void {
    const fueEdicion = this.editandoId !== null;
    const dto: CrearEstudiante = { nombre: this.form.nombre.trim(), correo: this.form.correo.trim(), cursoId: Number(this.form.cursoId) };
    this.guardando = true; this.aviso = '';
    const peticion = fueEdicion ? this.service.actualizar(this.editandoId!, dto) : this.service.crear(dto);
    peticion.subscribe({ next: () => { this.guardando = false; this.cancelar(); this.mostrarExito(fueEdicion ? 'Estudiante actualizado correctamente.' : 'Estudiante registrado correctamente.'); this.cargar(); }, error: e => { this.guardando = false; this.mostrarError(e); } });
  }
  eliminar(estudiante: Estudiante): void {
    if (!confirm(`¿Eliminar a ${estudiante.nombre}?`)) return;
    this.service.eliminar(estudiante.id).subscribe({ next: () => { this.mostrarExito('Estudiante eliminado.'); this.cargar(); }, error: e => this.mostrarError(e) });
  }
  private mostrarExito(texto: string): void { this.aviso = texto; this.tipoAviso = 'success'; }
  private mostrarError(error: unknown): void {
    const err = error as HttpErrorResponse;
    this.aviso = err.error?.mensaje ?? err.error?.title ?? (err.status === 0 ? 'No se pudo conectar con la API. Comprueba que el backend esté iniciado.' : 'Ocurrió un error al procesar la solicitud.');
    this.tipoAviso = 'error';
  }
}
