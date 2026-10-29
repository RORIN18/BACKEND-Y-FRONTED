import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { CrearEstudiante, Estudiante } from '../models/escuela.models';
@Injectable({ providedIn: 'root' })
export class EstudiantesService {
  private readonly http = inject(HttpClient);
  private readonly endpoint = 'http://localhost:5275/api/estudiantes';
  listar(): Observable<Estudiante[]> { return this.http.get<Estudiante[]>(this.endpoint); }
  crear(dto: CrearEstudiante): Observable<void> { return this.http.post<Estudiante>(this.endpoint, dto).pipe(map(() => undefined)); }
  actualizar(id: number, dto: CrearEstudiante): Observable<void> { return this.http.put<void>(`${this.endpoint}/${id}`, dto); }
  eliminar(id: number): Observable<void> { return this.http.delete<void>(`${this.endpoint}/${id}`); }
}
