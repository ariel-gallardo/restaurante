import { Component } from '@angular/core';

@Component({
  selector: 'app-v2-home',
  standalone: false,
  template: `
    <section style="padding: 2rem;">
      <h1>Nueva ruta en v2</h1>
      <p>Esta pantalla se renderiza desde Angular v2.</p>
    </section>
  `,
})
export class V2HomeComponent {}
