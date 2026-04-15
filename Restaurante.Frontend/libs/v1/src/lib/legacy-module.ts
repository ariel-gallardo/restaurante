import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LegacyHostComponent } from './legacy-host/legacy-host.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [LegacyHostComponent],
  imports: [CommonModule, 
    RouterModule.forChild([{ path: '**', component: LegacyHostComponent }])]
})
export class LegacyModule {}
