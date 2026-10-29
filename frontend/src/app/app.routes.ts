import { Routes } from '@angular/router';
export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'cursos' },
  { path: 'cursos', loadComponent: () => import('./pages/cursos/cursos.page').then(m => m.CursosPage) },
  { path: 'estudiantes', loadComponent: () => import('./pages/estudiantes/estudiantes.page').then(m => m.EstudiantesPage) },
  { path: '**', redirectTo: 'cursos' }
];
