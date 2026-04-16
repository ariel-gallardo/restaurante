import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { sharedShellEventTracked } from '@org/shared-shell';
import { legacyHostMounted } from '@org/legacy-host';
import { map, tap } from 'rxjs/operators';
import { v1ShellInitialized } from './v1-shell.actions';

@Injectable()
export class V1ShellEffects {
  private readonly actions$ = inject(Actions);

  logInit$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(v1ShellInitialized),
        tap(() => console.log('[NgRx][V1 Shell] initialized'))
      ),
    { dispatch: false }
  );

  logMount$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(legacyHostMounted),
        tap(() => console.log('[NgRx][V1 Shell] legacy app mounted'))
      ),
    { dispatch: false }
  );

  trackSharedInit$ = createEffect(() =>
    this.actions$.pipe(
      ofType(v1ShellInitialized),
      tap(() => console.log('[NgRx][Shared Shell] tracking v1 event')),
      // Dispatch a shared event so v1 and v2 can share aggregate state.
      map(() => sharedShellEventTracked({ source: 'v1' }))
    )
  );
}
