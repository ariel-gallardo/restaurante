import { createAction, props } from '@ngrx/store';

export const sharedShellEventTracked = createAction(
  '[Shared Shell] Event Tracked',
  props<{ source: 'v1' | 'v2' }>()
);
