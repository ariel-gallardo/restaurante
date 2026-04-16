import { createReducer, on } from '@ngrx/store';
import { legacyHostMounted } from '@resto/legacy-host';
import { v1ShellInitialized } from './v1-shell.actions';

export const v1ShellFeatureKey = 'v1Shell';

export interface V1ShellState {
  initCount: number;
  mountedCount: number;
}

const initialState: V1ShellState = {
  initCount: 0,
  mountedCount: 0,
};

export const v1ShellReducer = createReducer(
  initialState,
  on(v1ShellInitialized, (state) => ({ ...state, initCount: state.initCount + 1 })),
  on(legacyHostMounted, (state) => ({ ...state, mountedCount: state.mountedCount + 1 }))
);
