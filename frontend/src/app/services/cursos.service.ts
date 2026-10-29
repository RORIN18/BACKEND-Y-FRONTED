import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { CrearCurso, Curso } from '../models/escuela.models';
@Injectable({ providedIn: 'root' })
export class CursosService {
  private readonly http = inject(HttpClient);
  private readonly endpoint = 'http://localhost:5275/api/cursos';
  listar(): Observable<Curso[]> { return this.http.get<Curso[]>(this.endpoint); }
  crear(dto: CrearCurso): Observable<void> { return this.http.post<Curso>(this.endpoint, dto).pipe(map(() => undefined)); }
  actualizar(id: number, dto: CrearCurso): Observable<void> { return this.http.put<void>(`${this.endpoint}/${id}`, dto); }
  eliminar(id: number): Observable<void> { return this.http.delete<void>(`${this.endpoint}/${id}`); }
}
