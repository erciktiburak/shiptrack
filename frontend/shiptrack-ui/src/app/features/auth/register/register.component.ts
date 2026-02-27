import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  template: `
    <div class="auth-wrapper">
      <div class="auth-card">
        <h2>🚢 Create Account</h2>
        <div class="form-group">
          <label>Username</label>
          <input [(ngModel)]="username" placeholder="Enter username" />
        </div>
        <div class="form-group">
          <label>Email</label>
          <input type="email" [(ngModel)]="email" placeholder="Enter email" />
        </div>
        <div class="form-group">
          <label>Password</label>
          <input type="password" [(ngModel)]="password" placeholder="Enter password" />
        </div>
        <button (click)="register()">Register</button>
        <p>Already have account? <a routerLink="/auth/login">Login</a></p>
      </div>
    </div>
  `,
  styles: [`
    .auth-wrapper { display: flex; justify-content: center; align-items: center; height: 100vh; background: #f0f2f5; }
    .auth-card { background: white; padding: 2rem; border-radius: 12px; width: 360px; box-shadow: 0 4px 20px rgba(0,0,0,0.1); }
    h2 { text-align: center; margin-bottom: 1.5rem; }
    .form-group { margin-bottom: 1rem; }
    label { display: block; margin-bottom: 4px; font-weight: 500; }
    input { width: 100%; padding: 0.75rem; border: 1px solid #ddd; border-radius: 8px; box-sizing: border-box; }
    button { width: 100%; padding: 0.75rem; background: #2c3e50; color: white; border: none; border-radius: 8px; cursor: pointer; font-size: 1rem; margin-top: 0.5rem; }
    p { text-align: center; margin-top: 1rem; }
  `]
})
export class RegisterComponent {
  username = '';
  email = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    this.authService.register(this.username, this.email, this.password).subscribe({
      next: () => this.router.navigate(['/dashboard'])
    });
  }
}
