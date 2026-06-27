import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { selectV2ShellBootCount } from '../state/v2-shell.selectors';
import { v2ShellBootstrapped } from '../state/v2-shell.actions';
import {
  selectSharedUser,
  selectSharedIsAuthenticated,
  selectSharedOrderStatus,
  selectSharedCartCount,
  selectSharedTheme,
  selectSharedLanguage,
  clearSharedUser,
  setSharedTheme,
  setSharedLanguage,
  SharedUser,
  TRANSLATIONS
} from '@org/shared-shell';

@Component({
  selector: 'v2-dashboard',
  standalone: false,
  templateUrl: './dashboard.ng.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit, OnDestroy {
  bootCount$: Observable<number>;
  user$: Observable<SharedUser | null>;
  isAuthenticated$: Observable<boolean>;
  orderStatus$: Observable<string>;
  cartCount$: Observable<number>;
  theme$: Observable<'light' | 'dark'>;
  language$: Observable<'es' | 'en'>;
  
  showNavbar = false;
  isMenuCollapsed = true;
  currentTheme: 'light' | 'dark' = 'light';
  private themeSubscription?: Subscription;

  constructor(
    private readonly store: Store,
    private readonly router: Router
  ) {
    this.bootCount$ = this.store.select(selectV2ShellBootCount);
    this.user$ = this.store.select(selectSharedUser);
    this.isAuthenticated$ = this.store.select(selectSharedIsAuthenticated);
    this.orderStatus$ = this.store.select(selectSharedOrderStatus);
    this.cartCount$ = this.store.select(selectSharedCartCount);
    this.theme$ = this.store.select(selectSharedTheme);
    this.language$ = this.store.select(selectSharedLanguage);

    // Initial check
    this.updateNavbarVisibility(this.router.url);

    // Track route changes
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.updateNavbarVisibility(event.urlAfterRedirects || event.url);
      this.isMenuCollapsed = true; // Close menu on navigation
    });

    (window as any).angularStore = this.store;
    this.store.subscribe((state) => {
      (window as any).angularStoreState = state;
    });

    // Handle theme setting to body tag
    this.themeSubscription = this.theme$.subscribe(theme => {
      this.currentTheme = theme;
      this.applyTheme(theme);
    });
  }

  private applyTheme(theme: 'light' | 'dark'): void {
    if (theme === 'dark') {
      document.body.classList.add('dark-theme');
      document.body.classList.remove('light-theme');
    } else {
      document.body.classList.add('light-theme');
      document.body.classList.remove('dark-theme');
    }
  }

  private updateNavbarVisibility(url: string): void {
    this.showNavbar = true;
  }

  getStatusClass(status: string | null): string {
    if (!status) return 'badge-searching';
    switch (status) {
      case 'SEARCHING':
        return 'badge-searching';
      case 'CREATED':
        return 'badge-created';
      case 'PREPAIRING':
        return 'badge-preparing';
      case 'RECEPTION':
        return 'badge-reception';
      case 'DELIVERY':
        return 'badge-delivery';
      case 'CLIENT_DOOR':
        return 'badge-door';
      case 'DONE':
        return 'badge-done';
      case 'CANCEL':
      case 'CANCELED':
        return 'badge-canceled';
      default:
        return 'badge-searching';
    }
  }

  getTranslation(key: string, lang: 'es' | 'en' | null): string {
    const activeLang = lang || 'es';
    const dict = TRANSLATIONS[activeLang] || TRANSLATIONS.es;
    return (dict as any)[key] || key;
  }

  getTranslatedStatus(status: string | null, lang: 'es' | 'en' | null): string {
    if (!status) return '';
    const activeLang = lang || 'es';
    const dict = TRANSLATIONS[activeLang] || TRANSLATIONS.es;
    const keysMap: Record<string, string> = {
      'SEARCHING': 'SEARCHING',
      'CREATED': 'CREATED',
      'PREPAIRING': 'PREPAIRING',
      'RECEPTION': 'RECEPTION',
      'DELIVERY': 'DELIVERY',
      'CLIENT_DOOR': 'CLIENT_DOOR',
      'DONE': 'DONE',
      'CANCEL': 'CANCELED',
      'CANCELED': 'CANCELED'
    };
    const mappedKey = keysMap[status] || status;
    return (dict as any)[mappedKey] || status;
  }

  ngOnInit(): void {
    this.store.dispatch(v2ShellBootstrapped());
  }

  ngOnDestroy(): void {
    if (this.themeSubscription) {
      this.themeSubscription.unsubscribe();
    }
  }

  logout(): void {
    this.store.dispatch(clearSharedUser());
    this.router.navigate(['/login']);
  }

  goTo(path: string): void {
    this.isMenuCollapsed = true;
    this.router.navigate([path]);
  }

  toggleTheme(): void {
    const nextTheme = this.currentTheme === 'dark' ? 'light' : 'dark';
    this.store.dispatch(setSharedTheme({ theme: nextTheme }));
  }

  setLanguage(lang: 'es' | 'en'): void {
    this.store.dispatch(setSharedLanguage({ language: lang }));
  }

  toggleMenu(): void {
    this.isMenuCollapsed = !this.isMenuCollapsed;
  }

  get currentYear() {
    return new Date().getFullYear();
  }
}
