import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../authentication/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isAuthenticated: boolean = false;

  constructor(private router: Router,
              private authService:AuthService) { }

  ngOnInit(): void {
     this.authService.isLoggedIn$.subscribe(
      (isAuth: boolean) => {
        this.isAuthenticated = isAuth;
      });
  }

  isRouteActive(routePath: string): boolean {
    return this.router.url.startsWith(routePath);
  }

  logout()
  {
    this.authService.logOut();
    this.router.navigate(['/login'])
  }
}
