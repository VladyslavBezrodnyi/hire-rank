import {Component, OnInit} from '@angular/core';
import {AuthorizationService} from './core/services/authorization.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isCollapsed = false;
  isLoginFormVisible = false;
  isRegisterFormVisible = false;
  isLogged = false;
  currentYear = (new Date()).getFullYear();

  constructor(private authorizationService: AuthorizationService, private router: Router) {}

  ngOnInit(): void {
    this.authorizationService.isLoggedIn.subscribe(x => this.isLogged = x);
  }

  showLoginForm() {
    this.isLoginFormVisible = true;
  }

  showRegisterForm() {
    this.isRegisterFormVisible = true;
  }

  hideLoginForm() {
    this.isLoginFormVisible = false;
    this.isLogged = true;
  }

  hideRegistrationForm() {
    this.isRegisterFormVisible = false;
  }

  handleCancel(form: string): void {
    if (form == "login")
      this.isLoginFormVisible = false;
    else
      this.isRegisterFormVisible = false;
  }

  logOut() {
    this.authorizationService.logout();
    this.isLogged = false;
    this.router.navigate(['/']);
  }
}
