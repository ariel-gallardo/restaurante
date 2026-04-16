import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectLegacyMountedCount, selectV1ShellInitCount } from './state/v1-shell.selectors';
import { v1ShellInitialized } from './state/v1-shell.actions';

@Component({
  selector: 'v1-shell',
  standalone: false,
  templateUrl: './v1-shell.component.html',
})
export class V1ShellComponent implements OnInit {
  initCount$: Observable<number>;
  mountedCount$: Observable<number>;

  constructor(private readonly store: Store) {
    this.initCount$ = this.store.select(selectV1ShellInitCount);
    this.mountedCount$ = this.store.select(selectLegacyMountedCount);
  }

  ngOnInit(): void {
    this.store.dispatch(v1ShellInitialized());
  }
}
