import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import {
  selectSharedUser,
  setSharedUser,
  clearSharedUser,
  SharedUser
} from '@org/shared-shell';

@Component({
  selector: 'v2-profile',
  standalone: false,
  templateUrl: './profile.ng.html',
  styleUrl: './profile.scss',
})
export class ProfileComponent implements OnInit, OnDestroy {
  user$: Observable<SharedUser | null>;
  profileForm!: FormGroup;
  currentUser: SharedUser | null = null;
  private userSubscription!: Subscription;
  isSaving = false;
  successMessage = '';
  errorMessage = '';

  constructor(
    private readonly store: Store,
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly http: HttpClient
  ) {
    this.user$ = this.store.select(selectSharedUser);
    this.initForm();
  }

  private initForm(): void {
    this.profileForm = this.fb.group({
      nombre: ['', [Validators.required]],
      apellido: ['', [Validators.required]],
      correo: ['', [Validators.required, Validators.email]],
      calle: ['', [Validators.required]],
      numero: ['', [Validators.required]],
      telefono: [''],
      rol: [{ value: '', disabled: true }]
    });
  }

  ngOnInit(): void {
    this.userSubscription = this.user$.subscribe((user) => {
      if (user) {
        this.currentUser = user;
        
        // Split nombreCompleto
        const nameParts = (user.nombreCompleto || '').trim().split(' ');
        const nombre = nameParts[0] || '';
        const apellido = nameParts.slice(1).join(' ') || '';

        // Split domicilio
        const streetParts = (user.domicilio || '').trim().split(' ');
        const calle = streetParts.slice(0, -1).join(' ') || '';
        const numero = streetParts[streetParts.length - 1] || '';

        this.profileForm.patchValue({
          nombre,
          apellido,
          correo: user.correo || '',
          calle,
          numero,
          telefono: user.telefono || '',
          rol: user.tipoDeUsuario || 'USER'
        });
      } else {
        this.router.navigate(['/login']);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }

  saveChanges(): void {
    if (this.profileForm.invalid || !this.currentUser) {
      return;
    }

    this.isSaving = true;
    this.successMessage = '';
    this.errorMessage = '';

    const formVal = this.profileForm.value;
    const toSend: any = {};
    let hasChanges = false;

    const newFullName = `${formVal.nombre.trim()} ${formVal.apellido.trim()}`;
    if (newFullName !== this.currentUser.nombreCompleto) {
      toSend.nombre = formVal.nombre.trim();
      toSend.apellido = formVal.apellido.trim();
      hasChanges = true;
    }

    const newAddress = `${formVal.calle.trim()} ${formVal.numero.trim()}`;
    if (newAddress !== this.currentUser.domicilio) {
      toSend.calle = formVal.calle.trim();
      toSend.numero = formVal.numero.trim();
      hasChanges = true;
    }

    if (formVal.correo.trim() !== this.currentUser.correo) {
      toSend.correo = formVal.correo.trim();
      hasChanges = true;
    }

    if (formVal.telefono?.trim() !== (this.currentUser.telefono || '')) {
      toSend.telefono = formVal.telefono.trim();
      hasChanges = true;
    }

    if (!hasChanges) {
      this.successMessage = 'No se detectaron cambios para guardar.';
      this.isSaving = false;
      return;
    }

    const token = this.getCookie('auth_token');
    const headers = new HttpHeaders({
      'Authorization': token || '',
      'Content-Type': 'application/json'
    });

    this.http.put('https://localhost:44330/api/user', toSend, { headers }).subscribe({
      next: (response: any) => {
        this.isSaving = false;
        this.successMessage = 'Perfil actualizado correctamente.';
        
        // Construct updated user info
        const updatedUser: SharedUser = {
          ...this.currentUser!,
          nombreCompleto: newFullName,
          correo: formVal.correo.trim(),
          domicilio: newAddress,
          telefono: formVal.telefono?.trim() || ''
        };

        // Update localStorage and NgRx store
        localStorage.setItem('userInfo', JSON.stringify(updatedUser));
        this.store.dispatch(setSharedUser({ user: updatedUser }));
      },
      error: (err) => {
        this.isSaving = false;
        this.errorMessage = 'Ocurrió un error al actualizar el perfil. Por favor, intente nuevamente.';
        console.error('Error al actualizar el perfil', err);
      }
    });
  }

  logout(): void {
    this.store.dispatch(clearSharedUser());
    this.router.navigate(['/login']);
  }

  private getCookie(name: string): string {
    if (typeof document === 'undefined') return '';
    const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]*)'));
    return match ? decodeURIComponent(match[2]) : '';
  }
}
