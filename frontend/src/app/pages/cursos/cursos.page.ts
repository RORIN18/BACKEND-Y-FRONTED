import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { AutoFocusDirective } from '../../shared/directives/auto-focus.directive';
import { InicialesPipe } from '../../shared/pipes/iniciales.pipe';
import { Curso } from '../../models/escuela.models';
import { CursosService } from '../../services/cursos.service';
@Component({ selector: 'app-cursos-page', standalone: true, imports: [FormsModule, AutoFocusDirective, InicialesPipe], templateUrl: './cursos.page.html' })
export class CursosPage implements OnInit {
  private readonly service = inject(CursosService);
  cursos: Curso[] = [];
  filtro = '';
  cargando = true;
  guardando = false;
  editandoId: number | null = null;
  aviso = '';
  tipoAviso: 'error' | 'success' = 'success';
  form = { nombre: '', descripcion: '' };
  get cursosFiltrados(): Curso[] {
    const q = this.filtro.trim().toLocaleLowerCase();
    return this.cursos.filter(c => `${c.nombre} ${c.descripcion ?? ''}`.toLocaleLowerCase().includes(q));
  }
  ngOnInit(): void { this.cargar(); }
  cargar(): void {
    this.cargando = true;
    this.service.listar().subscribe({ next: data => { this.cursos = data; this.cargando = false; }, error: e => { this.mostrarError(e); this.cargando = false; } });
  }
  editar(curso: Curso): void { this.editandoId = curso.id; this.form = { nombre: curso.nombre, descripcion: curso.descripcion ?? '' }; this.aviso = ''; }
  cancelar(): void { this.editandoId = null; this.form = { nombre: '', descripcion: '' }; }
  guardar(): void {
    const fueEdicion = this.editandoId !== null;
    const id = this.editandoId;
    const dto = { nombre: this.form.nombre.trim(), descripcion: this.form.descripcion.trim() || null };
    this.guardando = true; this.aviso = '';
    const peticion = fueEdicion ? this.service.actualizar(id!, dto) : this.service.crear(dto);
    peticion.subscribe({ next: () => { this.guardando = false; this.cancelar(); this.mostrarExito(fueEdicion ? 'Curso actualizado correctamente.' : 'Curso creado correctamente.'); this.cargar(); }, error: e => { this.guardando = false; this.mostrarError(e); } });
  }
  eliminar(curso: Curso): void {
    if (!confirm(`¿Eliminar el curso «${curso.nombre}»?`)) return;
    this.service.eliminar(curso.id).subscribe({ next: () => { this.mostrarExito('Curso eliminado.'); this.cargar(); }, error: e => this.mostrarError(e) });
  }
  private mostrarExito(texto: string): void { this.aviso = texto; this.tipoAviso = 'success'; }
  private mostrarError(error: unknown): void {
    const err = error as HttpErrorResponse;
    this.aviso = err.error?.mensaje ?? err.error?.title ?? (err.status === 0 ? 'No se pudo conectar con la API. Comprueba que el backend esté iniciado.' : 'Ocurrió un error al procesar la solicitud.');
    this.tipoAviso = 'error';
  }
}

