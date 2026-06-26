import { Route } from '@angular/router';
import { ProfileComponent } from '../profile/profile';

export const shellRoutes: Route[] = [
  { path: 'profile', component: ProfileComponent },
  { path: '**', loadChildren: () => import('@resto/v1').then((m) => m.LegacyModule) },
];
