import { createFeatureSelector, createSelector } from '@ngrx/store';
import { sharedShellFeatureKey, SharedShellState } from './shared-shell.reducer';

export const selectSharedShellState = createFeatureSelector<SharedShellState>(
  sharedShellFeatureKey
);

export const selectSharedShellTotalEvents = createSelector(
  selectSharedShellState,
  (state) => state.totalEvents
);

export const selectSharedOrderStatus = createSelector(
  selectSharedShellState,
  (state) => state.ORDER_STATUS
);

export const selectSharedShellV1Events = createSelector(
  selectSharedShellState,
  (state) => state.bySource.v1
);

export const selectSharedShellV2Events = createSelector(
  selectSharedShellState,
  (state) => state.bySource.v2
);
