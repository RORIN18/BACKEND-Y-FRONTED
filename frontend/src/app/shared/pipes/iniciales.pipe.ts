import { Pipe, PipeTransform } from '@angular/core';
@Pipe({ name: 'iniciales', standalone: true })
export class InicialesPipe implements PipeTransform {
  transform(nombre: string | null | undefined): string {
    return (nombre ?? '').trim().split(/\s+/).filter(Boolean).slice(0, 2).map(parte => parte[0].toLocaleUpperCase()).join('') || '•';
  }
}
