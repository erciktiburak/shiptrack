import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink],
  template: `<p>Login page — <a routerLink="/dashboard">Go to Dashboard</a></p>`
})
export class LoginComponent {}
