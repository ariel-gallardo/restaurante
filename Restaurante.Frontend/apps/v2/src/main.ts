import { platformBrowser } from '@angular/platform-browser';
import { ShellModule } from '@resto/v2';

platformBrowser()
  .bootstrapModule(ShellModule)
  .catch((err) => console.error(err));
