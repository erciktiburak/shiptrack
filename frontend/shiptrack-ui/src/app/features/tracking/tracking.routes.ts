import { Routes } from '@angular/router';

export const TRACKING_ROUTES: Routes = [
  { path: '', loadComponent: () => import('./tracking.component').then(m => m.TrackingComponent) }
];
