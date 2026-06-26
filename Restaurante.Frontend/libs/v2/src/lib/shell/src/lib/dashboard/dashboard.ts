import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { selectV2ShellBootCount } from '../state/v2-shell.selectors';
import { v2ShellBootstrapped } from '../state/v2-shell.actions';
import {
  selectSharedUser,
  selectSharedIsAuthenticated,
  selectSharedOrderStatus,
  selectSharedCartCount,
  clearSharedUser,
  SharedUser
} from '@org/shared-shell';

@Component({
  selector: 'v2-dashboard',
  standalone: false,
  templateUrl: './dashboard.ng.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  bootCount$: Observable<number>;
  user$: Observable<SharedUser | null>;
  isAuthenticated$: Observable<boolean>;
  orderStatus$: Observable<string>;
  cartCount$: Observable<number>;
  showNavbar = false;

  constructor(
    private readonly store: Store,
    private readonly router: Router
  ) {
    this.bootCount$ = this.store.select(selectV2ShellBootCount);
    this.user$ = this.store.select(selectSharedUser);
    this.isAuthenticated$ = this.store.select(selectSharedIsAuthenticated);
    this.orderStatus$ = this.store.select(selectSharedOrderStatus);
    this.cartCount$ = this.store.select(selectSharedCartCount);

    // Initial check
    this.updateNavbarVisibility(this.router.url);

    // Track route changes
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.updateNavbarVisibility(event.urlAfterRedirects || event.url);
    });

    (window as any).angularStore = this.store;
    this.store.subscribe((state) => {
      (window as any).angularStoreState = state;
    });
  }

  private updateNavbarVisibility(url: string): void {
    // Show the v2 navigation bar globally when running v2
    this.showNavbar = true;
  }

  getStatusClass(status: string | null): string {
    if (!status) return 'text-bg-light';
    switch (status) {
      case 'CREATED':
        return 'text-bg-secondary';
      case 'PREPAIRING':
        return 'text-bg-primary';
      case 'RECEPTION':
        return 'text-bg-info';
      case 'DELIVERY':
        return 'text-bg-warning';
      case 'CLIENT_DOOR':
        return 'text-bg-dark';
      case 'DONE':
        return 'text-bg-success';
      case 'CANCEL':
      case 'CANCELED':
        return 'text-bg-danger';
      default:
        return 'text-bg-light';
    }
  }

  ngOnInit(): void {
    this.store.dispatch(v2ShellBootstrapped());
  }

  logout(): void {
    this.store.dispatch(clearSharedUser());
    this.router.navigate(['/login']);
  }

  goTo(path: string): void {
    this.router.navigate([path]);
  }
}

