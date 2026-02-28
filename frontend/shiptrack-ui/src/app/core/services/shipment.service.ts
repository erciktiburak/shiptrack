import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Shipment {
  id: string;
  status: 'Pending' | 'InTransit' | 'Delivered';
  [key: string]: unknown;
}

@Injectable({ providedIn: 'root' })
export class ShipmentService {
  private apiUrl = 'http://localhost:5000/api/shipments';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Shipment[]> {
    return this.http.get<Shipment[]>(this.apiUrl);
  }
}
