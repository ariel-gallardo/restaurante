import { Route } from '@angular/router';

export const shellRoutes: Route[] = [
  { path: '**', loadChildren: () => import('@resto/v1').then((m) => m.LegacyModule) },
];
