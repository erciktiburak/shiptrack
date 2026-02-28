import { Component, OnInit, signal } from '@angular/core';
import { ShipmentService } from '../../../core/services/shipment.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  template: `
    <div>
      <h1>📊 Dashboard</h1>
      <div class="stats-grid">
        <div class="stat-card">
          <h3>Total Shipments</h3>
          <span class="number">{{ totalShipments() }}</span>
        </div>
        <div class="stat-card pending">
          <h3>Pending</h3>
          <span class="number">{{ pending() }}</span>
        </div>
        <div class="stat-card transit">
          <h3>In Transit</h3>
          <span class="number">{{ inTransit() }}</span>
        </div>
        <div class="stat-card delivered">
          <h3>Delivered</h3>
          <span class="number">{{ delivered() }}</span>
        </div>
      </div>
    </div>
  `,
  styles: [`
    h1 { margin-bottom: 2rem; color: #2c3e50; }
    .stats-grid { display: grid; grid-template-columns: repeat(4, 1fr); gap: 1.5rem; }
    .stat-card { background: white; padding: 1.5rem; border-radius: 12px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); text-align: center; }
    .stat-card h3 { margin: 0 0 0.5rem; color: #666; font-size: 0.9rem; }
    .number { font-size: 2.5rem; font-weight: bold; color: #2c3e50; }
    .pending .number { color: #f39c12; }
    .transit .number { color: #3498db; }
    .delivered .number { color: #27ae60; }
  `]
})
export class DashboardComponent implements OnInit {
  totalShipments = signal(0);
  pending = signal(0);
  inTransit = signal(0);
  delivered = signal(0);

  constructor(private shipmentService: ShipmentService) {}

  ngOnInit() {
    this.shipmentService.getAll().subscribe(shipments => {
      this.totalShipments.set(shipments.length);
      this.pending.set(shipments.filter(s => s.status === 'Pending').length);
      this.inTransit.set(shipments.filter(s => s.status === 'InTransit').length);
      this.delivered.set(shipments.filter(s => s.status === 'Delivered').length);
    });
  }
}
