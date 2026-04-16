import { createReducer, on } from '@ngrx/store';
import { sharedShellEventTracked } from './shared-shell.actions';

export const sharedShellFeatureKey = 'sharedShell';

export interface SharedShellState {
  totalEvents: number;
  bySource: {
    v1: number;
    v2: number;
  };
}

const initialState: SharedShellState = {
  totalEvents: 0,
  bySource: {
    v1: 0,
    v2: 0,
  },
};

export const sharedShellReducer = createReducer(
  initialState,
  on(sharedShellEventTracked, (state, { source }) => ({
    ...state,
    totalEvents: state.totalEvents + 1,
    bySource: {
      ...state.bySource,
      [source]: state.bySource[source] + 1,
    },
  }))
);
