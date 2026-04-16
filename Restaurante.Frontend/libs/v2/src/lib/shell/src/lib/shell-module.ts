import { isDevMode, NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { SharedShellModule } from '@org/shared-shell';
import { Dashboard } from './dashboard/dashboard';
import { shellRoutes } from './dashboard/dashboard.routes';
import { V2ShellEffects } from './state/v2-shell.effects';
import { v2ShellFeatureKey, v2ShellReducer } from './state/v2-shell.reducer';

@NgModule({
  declarations: [Dashboard],
  imports: [
    BrowserModule,
    SharedShellModule,
    RouterModule.forRoot(shellRoutes),
    StoreModule.forRoot({
      [v2ShellFeatureKey]: v2ShellReducer,
    }),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: !isDevMode(),
      autoPause: true,
    }),
    EffectsModule.forRoot([V2ShellEffects]),
  ],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [Dashboard],
})
export class ShellModule {}
