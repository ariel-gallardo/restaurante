import { createFeatureSelector, createSelector } from '@ngrx/store';
import { V1ShellState, v1ShellFeatureKey } from './v1-shell.reducer';

export const selectV1ShellState = createFeatureSelector<V1ShellState>(v1ShellFeatureKey);

export const selectV1ShellInitCount = createSelector(
  selectV1ShellState,
  (state) => state.initCount
);

export const selectLegacyMountedCount = createSelector(
  selectV1ShellState,
  (state) => state.mountedCount
);
