import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-legacy-host',
  standalone: false,
  template: `
    <div #legacyRoot></div>
    <p *ngIf="error" style="padding: 1rem; color: #b42318;">{{ error }}</p>
  `,
})
export class LegacyHostComponent implements AfterViewInit {
  @ViewChild('legacyRoot', { static: true })
  private legacyRoot!: ElementRef<HTMLElement>;

  error = '';

  async ngAfterViewInit(): Promise<void> {
    try {
      const remote = await import('v1/main');
      if (typeof remote.mount !== 'function') {
        throw new Error('El remoto v1/main no expone mount().');
      }

      remote.mount(this.legacyRoot.nativeElement);
    } catch (error) {
      this.error = 'No se pudo cargar la aplicación legacy (v1).';
      console.error(error);
    }
  }
}
