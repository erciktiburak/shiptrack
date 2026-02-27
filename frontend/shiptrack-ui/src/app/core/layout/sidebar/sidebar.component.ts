import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  template: `
    <nav class="sidebar">
      <div class="sidebar-logo">
        <span>🚢 ShipTrack</span>
      </div>
      <ul class="sidebar-menu">
        <li>
          <a routerLink="/dashboard" routerLinkActive="active">
            📊 Dashboard
          </a>
        </li>
        <li>
          <a routerLink="/shipments" routerLinkActive="active">
            📦 Shipments
          </a>
        </li>
        <li>
          <a routerLink="/tracking" routerLinkActive="active">
            🗺️ Tracking
          </a>
        </li>
      </ul>
    </nav>
  `,
  styles: [`
    .sidebar { width: 240px; background: #1a1a2e; color: white; padding: 1rem; }
    .sidebar-logo { font-size: 1.2rem; font-weight: bold; padding: 1rem; border-bottom: 1px solid #333; margin-bottom: 1rem; }
    .sidebar-menu { list-style: none; padding: 0; margin: 0; }
    .sidebar-menu li a { display: block; padding: 0.75rem 1rem; color: #aaa; text-decoration: none; border-radius: 8px; margin-bottom: 4px; }
    .sidebar-menu li a:hover, .sidebar-menu li a.active { background: #16213e; color: white; }
  `]
})
export class SidebarComponent {}
