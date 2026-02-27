import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  const router = inject(Router);
  const token = typeof localStorage !== 'undefined' ? localStorage.getItem('token') : null;
  if (token) return true;
  router.navigate(['/auth/login']);
  return false;
};
