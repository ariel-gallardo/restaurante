import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { RouterModule } from '@angular/router';
import { shellRoutes } from './dashboard/dashboard.routes';
import { Dashboard } from './dashboard/dashboard';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [Dashboard],
  imports: [BrowserModule, RouterModule.forRoot(shellRoutes)],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [Dashboard],
})
export class ShellModule { }
