import { Routes } from '@angular/router';
import { MainLayoutComponent } from '../../core/layout/main-layout/main-layout.component';

export const SHIPMENT_ROUTES: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', loadComponent: () => import('./shipment-list/shipment-list.component').then(m => m.ShipmentListComponent) },
      { path: 'create', loadComponent: () => import('./shipment-create/shipment-create.component').then(m => m.ShipmentCreateComponent) }
    ]
  }
];
