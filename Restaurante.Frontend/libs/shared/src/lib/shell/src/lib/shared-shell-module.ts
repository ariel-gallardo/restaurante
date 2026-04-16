import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { sharedShellRoutes } from './lib.routes';
import { SharedShellEffects } from './state/shared-shell.effects';
import { sharedShellFeatureKey, sharedShellReducer } from './state/shared-shell.reducer';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(sharedShellRoutes),
    StoreModule.forFeature(sharedShellFeatureKey, sharedShellReducer),
    EffectsModule.forFeature([SharedShellEffects]),
  ],
})
export class SharedShellModule {}
