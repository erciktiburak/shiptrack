import { Routes } from '@angular/router';
import { MainLayoutComponent } from '../../core/layout/main-layout/main-layout.component';

export const TRACKING_ROUTES: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', loadComponent: () => import('./tracking/tracking.component').then(m => m.TrackingComponent) }
    ]
  }
];
