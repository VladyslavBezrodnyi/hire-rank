import {Component, OnInit, Output, EventEmitter} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {LoginModel} from '../../../../shared/models/login.model';
import {Router} from '@angular/router';
import {Subscription} from 'rxjs';
import {AuthorizationService} from '../../../../core/services/authorization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  private subscription: Subscription;
  errorMessage: string;
  returnUrl: string = '/';

  @Output() logEvent = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder, private authorizationService: AuthorizationService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]]
    });
  }

  submitForm(): void {
    for (const i in this.loginForm.controls) {
      this.loginForm.controls[i].markAsDirty();
      this.loginForm.controls[i].updateValueAndValidity();
    }
    if (this.loginForm.status === "VALID") {
      console.log('userSubmit emited (onSubmit)');
    }

    let loginViewModel = this.loginForm.value as LoginModel;
    if (this.loginForm.valid) {
      this.login(loginViewModel);

      this.logEvent.emit(true);
      this.router.navigate(['/']);
    }
  }

  login(loginViewModel: LoginModel): void {
    this.subscription = this.authorizationService.login(loginViewModel)
      .subscribe(
        res => this.router.navigate([this.returnUrl]),
        errors => this.errorMessage = errors.message);
  }
}
