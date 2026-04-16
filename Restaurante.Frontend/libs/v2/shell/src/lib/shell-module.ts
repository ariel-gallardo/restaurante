import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';
import { shellRoutes } from './dashboard/dashboard.routes';

@NgModule({
  declarations: [Dashboard],
  imports: [BrowserModule, RouterModule.forRoot(shellRoutes)],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [Dashboard],
})
export class ShellModule {}
