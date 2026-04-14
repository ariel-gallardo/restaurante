import { Route } from '@angular/router';
import { LegacyHostComponent } from './legacy-host.component';
import { V2HomeComponent } from './v2-home.component';

export const appRoutes: Route[] = [
	{
		path: 'v2/nueva-ruta',
		component: V2HomeComponent,
	},
	{
		path: '**',
		component: LegacyHostComponent,
	},
];
