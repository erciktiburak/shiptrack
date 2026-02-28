import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface TrackingRecord {
  id: string;
  trackingNumber: string;
  status: string;
  currentLocation: string;
  notes: string;
  recordedAt: string;
}

@Injectable({ providedIn: 'root' })
export class TrackingService {
  private apiUrl = 'http://localhost:5000/api/tracking';

  constructor(private http: HttpClient) {}

  getLatest(trackingNumber: string) {
    return this.http.get<TrackingRecord>(`${this.apiUrl}/${trackingNumber}/latest`);
  }

  getHistory(trackingNumber: string) {
    return this.http.get<TrackingRecord[]>(`${this.apiUrl}/${trackingNumber}/history`);
  }
}
