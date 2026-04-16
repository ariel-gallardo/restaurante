import { createFeatureSelector, createSelector } from '@ngrx/store';
import { V2ShellState, v2ShellFeatureKey } from './v2-shell.reducer';

export const selectV2ShellState = createFeatureSelector<V2ShellState>(v2ShellFeatureKey);

export const selectV2ShellBootCount = createSelector(
  selectV2ShellState,
  (state) => state.bootCount
);
