import { inject, Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { sharedShellEventTracked } from './shared-shell.actions';

@Injectable()
export class SharedShellEffects {
  private readonly actions$ = inject(Actions);

  logSharedEvents$ = createEffect(
    () => this.actions$.pipe(ofType(sharedShellEventTracked)),
    { dispatch: false }
  );
}
