import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { LegacyHostModule } from '@org/legacy-host';
import { SharedShellModule } from '@org/shared-shell';
import { v1ShellRoutes } from './lib.routes';
import { V1ShellComponent } from './v1-shell.component';
import { V1ShellEffects } from './state/v1-shell.effects';
import { v1ShellFeatureKey, v1ShellReducer } from './state/v1-shell.reducer';

@NgModule({
  declarations: [V1ShellComponent],
  imports: [
    CommonModule,
    LegacyHostModule,
    SharedShellModule,
    RouterModule.forChild(v1ShellRoutes),
    StoreModule.forFeature(v1ShellFeatureKey, v1ShellReducer),
    EffectsModule.forFeature([V1ShellEffects]),
  ],
})
export class V1ShellModule {}
