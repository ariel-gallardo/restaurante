import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LegacyHostComponent } from './legacy-host.component';

@NgModule({
  declarations: [LegacyHostComponent],
  imports: [CommonModule, RouterModule.forChild([{ path: '**', component: LegacyHostComponent }])],
  exports: [LegacyHostComponent],
})
export class LegacyHostModule {}
