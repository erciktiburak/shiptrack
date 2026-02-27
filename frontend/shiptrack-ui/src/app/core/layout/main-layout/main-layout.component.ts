import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { TopbarComponent } from '../topbar/topbar.component';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, TopbarComponent],
  template: `
    <div class="layout-wrapper">
      <app-topbar />
      <div class="layout-main">
        <app-sidebar />
        <div class="layout-content">
          <router-outlet />
        </div>
      </div>
    </div>
  `,
  styles: [`
    .layout-wrapper { display: flex; flex-direction: column; height: 100vh; }
    .layout-main { display: flex; flex: 1; overflow: hidden; }
    .layout-content { flex: 1; padding: 2rem; overflow-y: auto; background: #f8f9fa; }
  `]
})
export class MainLayoutComponent {}
