import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ShipmentService } from '../../../core/services/shipment.service';

@Component({
  selector: 'app-shipment-create',
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="create-wrapper">
      <h1>📦 New Shipment</h1>
      <div class="form-card">
        <div class="form-row">
          <div class="form-group">
            <label>Sender Name</label>
            <input [(ngModel)]="form.senderName" placeholder="Sender name" />
          </div>
          <div class="form-group">
            <label>Receiver Name</label>
            <input [(ngModel)]="form.receiverName" placeholder="Receiver name" />
          </div>
        </div>
        <div class="form-row">
          <div class="form-group">
            <label>Origin Port</label>
            <input [(ngModel)]="form.originPort" placeholder="e.g. Famagusta" />
          </div>
          <div class="form-group">
            <label>Destination Port</label>
            <input [(ngModel)]="form.destinationPort" placeholder="e.g. Piraeus" />
          </div>
        </div>
        <div class="form-group">
          <label>Weight (kg)</label>
          <input type="number" [(ngModel)]="form.weightKg" placeholder="0.00" />
        </div>
        <div class="actions">
          <button class="cancel" (click)="router.navigate(['/shipments'])">Cancel</button>
          <button (click)="create()">Create Shipment</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    h1 { margin-bottom: 1.5rem; }
    .form-card { background: white; padding: 2rem; border-radius: 12px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); max-width: 700px; }
    .form-row { display: grid; grid-template-columns: 1fr 1fr; gap: 1rem; }
    .form-group { margin-bottom: 1rem; }
    label { display: block; margin-bottom: 4px; font-weight: 500; color: #555; }
    input { width: 100%; padding: 0.75rem; border: 1px solid #ddd; border-radius: 8px; box-sizing: border-box; }
    .actions { display: flex; gap: 1rem; justify-content: flex-end; margin-top: 1rem; }
    button { padding: 0.75rem 1.5rem; background: #2c3e50; color: white; border: none; border-radius: 8px; cursor: pointer; }
    button.cancel { background: #eee; color: #333; }
  `]
})
export class ShipmentCreateComponent {
  form = { senderName: '', receiverName: '', originPort: '', destinationPort: '', weightKg: 0 };

  constructor(private shipmentService: ShipmentService, public router: Router) {}

  create() {
    this.shipmentService.create(this.form).subscribe({
      next: () => this.router.navigate(['/shipments'])
    });
  }
}
