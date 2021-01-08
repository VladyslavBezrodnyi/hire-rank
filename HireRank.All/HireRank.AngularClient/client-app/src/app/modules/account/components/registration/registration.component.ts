import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {RegisterStudentModel} from '../../../../shared/models/register-student.model';
import {RegisterEmployerModel} from '../../../../shared/models/register-employer.model';
import {RegisterService} from '../../../../core/services/register.service';
import {LoginModel} from '../../../../shared/models/login.model';
import {AuthorizationService} from '../../../../core/services/authorization.service';
import {Router} from '@angular/router';
import {NameRegex} from '../../../../shared/regexes/name-regex';
import {PasswordRegex} from '../../../../shared/regexes/password-regex';
import {PhoneRegex} from '../../../../shared/regexes/phone-regex';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  studentRoleName = 'Student';
  employerRoleName = 'Employer';
  generalForm: FormGroup;
  radioValue: string = 'Student';
  errorMessage: string = null;
  form: FormGroup;

  @Output() registerEvent = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router,
              private authorizationService: AuthorizationService) {}

  ngOnInit(): void {
    this.generalForm = this.fb.group({
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required, Validators.pattern(PasswordRegex.Regex)]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]],
      role: [this.studentRoleName, [Validators.required]],
      firstName: [null, [Validators.required, Validators.pattern(NameRegex.Regex)]],
      middleName: [null, [Validators.required, Validators.pattern(NameRegex.Regex)]],
      lastName: [null, [Validators.required, Validators.pattern(NameRegex.Regex)]],
      dateOfBirth: [null, [Validators.required]],
      universityName: [null, [Validators.required]],
      major: [null, [Validators.required]],
      companyName: [null, [Validators.required]],
      companyDescription: [null, [Validators.required]],
      companyAddress: [null, [Validators.required]],
      contactPhoneNumber: [null, [Validators.required, Validators.pattern(PhoneRegex.Regex)]],
      siteUrl: [null, [Validators.required]],
    });

    this.generalForm.get('role').valueChanges.subscribe(value => {

      if (value == this.studentRoleName && this.radioValue != this.studentRoleName) {
        this.radioValue = this.studentRoleName;

      } else if (value == this.employerRoleName && this.radioValue != this.employerRoleName) {
        this.radioValue = this.employerRoleName;
      }
    });
  }

  submitForm(): void {
    if (this.radioValue == this.studentRoleName){
      this.fillStudentModel();
    } else if (this.radioValue == this.employerRoleName) {
      this.fillEmployerModel();
    }
  }

  fillStudentModel(){
    debugger;
      let registrationStudentModel = <RegisterStudentModel> {
        email: this.generalForm.value.email,
        password:  this.generalForm.value.password,
        firstName: this.generalForm.value.firstName,
        middleName: this.generalForm.value.middleName,
        lastName: this.generalForm.value.lastName,
        dateOfBirth: this.generalForm.value.dateOfBirth,
        universityName: this.generalForm.value.universityName,
        major: this.generalForm.value.major
      }
      this.registerStudent(registrationStudentModel);

  }

  fillEmployerModel(){
    let registrationEmployerModel = <RegisterEmployerModel> {
      email: this.generalForm.value.email,
      password:  this.generalForm.value.password,
      companyName: this.generalForm.value.companyName,
      companyAddress: this.generalForm.value.companyAddress,
      companyDescription: this.generalForm.value.companyDescription,
      contactPhoneNumber: this.generalForm.value.contactPhoneNumber,
      siteUrl: this.generalForm.value.siteUrl
    };
    this.registerEmployer(registrationEmployerModel);
  }

  registerStudent(registerViewModel: RegisterStudentModel): void {
    this.registerService.registerStudent(registerViewModel).subscribe(
      res => {
        this.authorizationService.loginToSystem(res, registerViewModel.email);
        this.registerEvent.emit(true);
      },
      errors => {
        this.errorMessage = errors.message;
        console.log(this.errorMessage);
      });
  }

  registerEmployer(registerViewModel: RegisterEmployerModel): void {
    this.registerService.registerEmployer(registerViewModel).subscribe(
      res => {
        this.authorizationService.loginToSystem(res, registerViewModel.email);
        this.registerEvent.emit(true);
      },
      errors => {
        this.errorMessage = errors.message;
        console.log(this.errorMessage);
      });
  }

  login(loginModel: LoginModel) {
    this.authorizationService.login(loginModel)
      .subscribe(x => this.router.navigate(['/']));
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.generalForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  };

  getCaptcha(e: MouseEvent): void {
    e.preventDefault();
  }
}
