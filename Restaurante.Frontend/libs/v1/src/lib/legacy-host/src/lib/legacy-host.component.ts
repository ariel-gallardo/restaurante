import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { legacyHostMounted } from './legacy-host.actions';

@Component({
  selector: 'legacy-host',
  standalone: false,
  templateUrl: './legacy-host.component.html',
})
export class LegacyHostComponent implements AfterViewInit {
  @ViewChild('legacyRoot', { static: true })
  private legacyRoot!: ElementRef<HTMLElement>;

  error = '';

  constructor(private readonly store: Store) {}

  async ngAfterViewInit(): Promise<void> {
    try {
      const remote = await import('v1/main');
      if (typeof remote.mount !== 'function') {
        throw new Error('El remoto v1/main no expone mount().');
      }
      remote.mount(this.legacyRoot.nativeElement);
      this.store.dispatch(legacyHostMounted());
    } catch (error) {
      this.error = 'No se pudo cargar la aplicacion legacy (v1).';
      console.error(error);
    }
  }
}
