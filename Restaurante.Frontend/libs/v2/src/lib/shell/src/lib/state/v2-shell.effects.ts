import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { sharedShellEventTracked } from '@org/shared-shell';
import { map, tap } from 'rxjs/operators';
import { v2ShellBootstrapped } from './v2-shell.actions';

@Injectable()
export class V2ShellEffects {
  private readonly actions$ = inject(Actions);

  logBootstrap$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(v2ShellBootstrapped),
        tap(() => console.log('[NgRx][V2 Shell] bootstrapped'))
      ),
    { dispatch: false }
  );

  trackSharedBootstrap$ = createEffect(() =>
    this.actions$.pipe(
      ofType(v2ShellBootstrapped),
      map(() => sharedShellEventTracked({ source: 'v2' }))
    )
  );
}
