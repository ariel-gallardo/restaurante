import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectV2ShellBootCount } from '../state/v2-shell.selectors';
import { v2ShellBootstrapped } from '../state/v2-shell.actions';

@Component({
  selector: 'v2-dashboard',
  standalone: false,
  templateUrl: './dashboard.ng.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  bootCount$: Observable<number>;

  constructor(private readonly store: Store) {
    this.bootCount$ = this.store.select(selectV2ShellBootCount);
  }

  ngOnInit(): void {
    this.store.dispatch(v2ShellBootstrapped());
  }
}
