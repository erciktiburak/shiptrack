import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { TrackingRecord, TrackingService } from '../../../core/services/tracking.service';

@Component({
  selector: 'app-tracking',
  standalone: true,
  imports: [FormsModule, DatePipe],
  template: `
    <div>
      <h1>🗺️ Track Shipment</h1>
      <div class="search-box">
        <input [(ngModel)]="trackingNumber" placeholder="Enter tracking number (e.g. SHT-20240101-ABCD1234)" />
        <button (click)="search()">Track</button>
      </div>

      @if (latest()) {
        <div class="status-card">
          <h2>{{ latest()!.trackingNumber }}</h2>
          <span [class]="'badge ' + latest()!.status.toLowerCase()">{{ latest()!.status }}</span>
          <p>📍 {{ latest()!.currentLocation }}</p>
          <p class="note">{{ latest()!.notes }}</p>
        </div>

        <h3>History</h3>
        <div class="timeline">
          @for (record of history(); track record.id) {
            <div class="timeline-item">
              <div class="dot"></div>
              <div class="content">
                <strong>{{ record.status }}</strong> — {{ record.currentLocation }}
                <br><small>{{ record.recordedAt | date:'medium' }}</small>
                @if (record.notes) { <p>{{ record.notes }}</p> }
              </div>
            </div>
          }
        </div>
      }

      @if (notFound()) {
        <div class="not-found">❌ Shipment not found</div>
      }
    </div>
  `,
  styles: [`
    h1 { margin-bottom: 1.5rem; }
    .search-box { display: flex; gap: 1rem; margin-bottom: 2rem; }
    .search-box input { flex: 1; padding: 0.75rem 1rem; border: 1px solid #ddd; border-radius: 8px; font-size: 1rem; }
    .search-box button { padding: 0.75rem 1.5rem; background: #2c3e50; color: white; border: none; border-radius: 8px; cursor: pointer; }
    .status-card { background: white; padding: 1.5rem; border-radius: 12px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); margin-bottom: 2rem; }
    .badge { padding: 4px 12px; border-radius: 20px; font-size: 0.85rem; font-weight: 600; }
    .badge.pending { background: #fff3cd; color: #856404; }
    .badge.intransit { background: #cce5ff; color: #004085; }
    .badge.delivered { background: #d4edda; color: #155724; }
    .timeline { background: white; padding: 1.5rem; border-radius: 12px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }
    .timeline-item { display: flex; gap: 1rem; margin-bottom: 1.5rem; }
    .dot { width: 12px; height: 12px; background: #2c3e50; border-radius: 50%; margin-top: 4px; flex-shrink: 0; }
    .not-found { background: #f8d7da; color: #721c24; padding: 1rem; border-radius: 8px; }
  `]
})
export class TrackingComponent {
  trackingNumber = '';
  latest = signal<TrackingRecord | null>(null);
  history = signal<TrackingRecord[]>([]);
  notFound = signal(false);

  constructor(private trackingService: TrackingService) {}

  search() {
    this.notFound.set(false);
    this.trackingService.getLatest(this.trackingNumber).subscribe({
      next: (data) => {
        this.latest.set(data);
        this.trackingService.getHistory(this.trackingNumber).subscribe(h => this.history.set(h));
      },
      error: () => {
        this.latest.set(null);
        this.notFound.set(true);
      }
    });
  }
}
