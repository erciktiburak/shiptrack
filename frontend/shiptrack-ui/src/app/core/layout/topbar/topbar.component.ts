import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-topbar',
  standalone: true,
  template: `
    <header class="topbar">
      <span>Maritime Logistics ERP</span>
      <button (click)="logout()">Logout</button>
    </header>
  `,
  styles: [`
    .topbar { display: flex; justify-content: space-between; align-items: center; padding: 1rem 2rem; background: white; border-bottom: 1px solid #e0e0e0; }
    button { padding: 0.5rem 1rem; background: #e74c3c; color: white; border: none; border-radius: 6px; cursor: pointer; }
  `]
})
export class TopbarComponent {
  constructor(private router: Router) {}
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/auth/login']);
  }
}
