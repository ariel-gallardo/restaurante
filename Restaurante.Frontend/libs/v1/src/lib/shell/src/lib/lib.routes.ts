import { Route } from '@angular/router';
import { V1ShellComponent } from './v1-shell.component';

export const v1ShellRoutes: Route[] = [
  { path: '**', component: V1ShellComponent },
];
