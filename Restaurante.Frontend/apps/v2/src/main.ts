import '@angular/compiler';
import { platformBrowser } from '@angular/platform-browser';
import { ShellModule } from '@resto/shell';

(window as any).__V2_RUNNING__ = true;

platformBrowser()
  .bootstrapModule(ShellModule)
  .catch((err) => console.error(err));
