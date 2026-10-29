export interface Curso { id: number; nombre: string; descripcion: string | null; }
export interface Estudiante { id: number; nombre: string; correo: string; cursoId: number; nombreCurso: string; }
export type CrearCurso = Omit<Curso, 'id'>;
export type CrearEstudiante = Omit<Estudiante, 'id' | 'nombreCurso'>;
