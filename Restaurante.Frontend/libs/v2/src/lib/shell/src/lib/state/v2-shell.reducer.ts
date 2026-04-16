import { createReducer, on } from '@ngrx/store';
import { v2ShellBootstrapped } from './v2-shell.actions';

export const v2ShellFeatureKey = 'v2Shell';

export interface V2ShellState {
  bootCount: number;
}

const initialState: V2ShellState = {
  bootCount: 0,
};

export const v2ShellReducer = createReducer(
  initialState,
  on(v2ShellBootstrapped, (state) => ({ ...state, bootCount: state.bootCount + 1 }))
);
