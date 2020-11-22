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
  isVisible = false;
  isLogged = false;

  constructor(private authorizationService: AuthorizationService, private router: Router) {}

  ngOnInit(): void {
    console.log(this.isVisible);
  }

  showLoginForm() {
    this.isVisible = true;
  }

  hideLoginForm() {
    this.isVisible = false;
    this.isLogged = true;
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }

  logOut() {
    this.authorizationService.logout();
    this.isLogged = false;
    this.router.navigate(['/']);
  }
}
