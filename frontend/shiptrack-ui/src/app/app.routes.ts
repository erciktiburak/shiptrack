import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadChildren: () => import('./features/dashboard/dashboard.routes').then(m => m.DASHBOARD_ROUTES)
  },
  {
    path: 'shipments',
    canActivate: [authGuard],
    loadChildren: () => import('./features/shipments/shipments.routes').then(m => m.SHIPMENT_ROUTES)
  },
  {
    path: 'tracking',
    canActivate: [authGuard],
    loadChildren: () => import('./features/tracking/tracking.routes').then(m => m.TRACKING_ROUTES)
  }
];
