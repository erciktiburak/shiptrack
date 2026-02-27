import { Routes } from '@angular/router';

export const SHIPMENT_ROUTES: Routes = [
  { path: '', loadComponent: () => import('./shipments-list.component').then(m => m.ShipmentsListComponent) }
];
