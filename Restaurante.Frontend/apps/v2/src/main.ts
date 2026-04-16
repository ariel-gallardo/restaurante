import { platformBrowser } from '@angular/platform-browser';
import { ShellModule } from '@resto/shell';

platformBrowser()
  .bootstrapModule(ShellModule)
  .catch((err) => console.error(err));
