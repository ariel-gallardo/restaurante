import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { sharedShellEventTracked, clearSharedUser, authLogout } from './shared-shell.actions';

@Injectable()
export class SharedShellEffects {
  private readonly actions$ = inject(Actions);
  private readonly router = inject(Router);

  logSharedEvents$ = createEffect(
    () => this.actions$.pipe(ofType(sharedShellEventTracked)),
    { dispatch: false }
  );

  logout$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(clearSharedUser, authLogout),
        tap(() => {
          try {
            document.cookie = 'auth_token=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT;';
            localStorage.removeItem('userInfo');
          } catch (e) {
            console.error('Error al limpiar la sesión en cookies/localStorage', e);
          }
          this.router.navigate(['/login']);
        })
      ),
    { dispatch: false }
  );
}

