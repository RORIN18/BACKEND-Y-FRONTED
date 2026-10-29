import { Directive, ElementRef, afterNextRender, inject } from '@angular/core';
@Directive({ selector: '[appAutoFocus]', standalone: true })
export class AutoFocusDirective {
  private readonly element = inject(ElementRef<HTMLInputElement>);
  constructor() { afterNextRender(() => this.element.nativeElement.focus()); }
}
