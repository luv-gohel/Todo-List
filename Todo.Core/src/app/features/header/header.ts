import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Auth } from '../../core/services/auth';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterOutlet,CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css',
})

export class Header {
  constructor(
    private authService: Auth,
    private router: Router
  ) {

  }
  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
