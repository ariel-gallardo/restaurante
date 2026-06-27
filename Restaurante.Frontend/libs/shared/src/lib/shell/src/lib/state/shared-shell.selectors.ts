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

export const selectSharedUser = createSelector(
  selectSharedShellState,
  (state) => state.user
);

export const selectSharedIsAuthenticated = createSelector(
  selectSharedShellState,
  (state) => state.isAuthenticated
);

export const selectSharedCartCount = createSelector(
  selectSharedShellState,
  (state) => state.cartCount
);

export const selectSharedTheme = createSelector(
  selectSharedShellState,
  (state) => state.theme
);

export const selectSharedLanguage = createSelector(
  selectSharedShellState,
  (state) => state.language
);


