import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Shipment, ShipmentService } from '../../../core/services/shipment.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-shipment-list',
  standalone: true,
  imports: [RouterLink, DatePipe],
  template: `
    <div>
      <div class="header">
        <h1>📦 Shipments</h1>
        <a routerLink="/shipments/create">+ New Shipment</a>
      </div>
      <div class="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Tracking No</th>
              <th>Sender</th>
              <th>Receiver</th>
              <th>Origin</th>
              <th>Destination</th>
              <th>Status</th>
              <th>Created</th>
            </tr>
          </thead>
          <tbody>
            @for (s of shipments(); track s.id) {
              <tr>
                <td><strong>{{ s.trackingNumber }}</strong></td>
                <td>{{ s.senderName }}</td>
                <td>{{ s.receiverName }}</td>
                <td>{{ s.originPort }}</td>
                <td>{{ s.destinationPort }}</td>
                <td><span [class]="'badge ' + s.status.toLowerCase()">{{ s.status }}</span></td>
                <td>{{ s.createdAt | date:'short' }}</td>
              </tr>
            }
          </tbody>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 1.5rem; }
    .header a { padding: 0.6rem 1.2rem; background: #2c3e50; color: white; text-decoration: none; border-radius: 8px; }
    .table-wrapper { background: white; border-radius: 12px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
    table { width: 100%; border-collapse: collapse; }
    th { background: #2c3e50; color: white; padding: 1rem; text-align: left; font-size: 0.85rem; }
    td { padding: 0.9rem 1rem; border-bottom: 1px solid #f0f0f0; font-size: 0.9rem; }
    .badge { padding: 4px 10px; border-radius: 20px; font-size: 0.8rem; font-weight: 600; }
    .badge.pending { background: #fff3cd; color: #856404; }
    .badge.intransit { background: #cce5ff; color: #004085; }
    .badge.delivered { background: #d4edda; color: #155724; }
    .badge.cancelled { background: #f8d7da; color: #721c24; }
  `]
})
export class ShipmentListComponent implements OnInit {
  shipments = signal<Shipment[]>([]);

  constructor(private shipmentService: ShipmentService) {}

  ngOnInit() {
    this.shipmentService.getAll().subscribe(data => this.shipments.set(data));
  }
}
