import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface Shipment {
  id: string;
  trackingNumber: string;
  senderName: string;
  receiverName: string;
  originPort: string;
  destinationPort: string;
  weightKg: number;
  status: string;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class ShipmentService {
  private apiUrl = 'http://localhost:5000/api/orders';

  constructor(private http: HttpClient) {}

  getAll() { return this.http.get<Shipment[]>(this.apiUrl); }
  getById(id: string) { return this.http.get<Shipment>(`${this.apiUrl}/${id}`); }
  create(dto: Partial<Shipment>) { return this.http.post<Shipment>(this.apiUrl, dto); }
  cancel(id: string) { return this.http.delete(`${this.apiUrl}/${id}/cancel`); }
}
