import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {RegisterStudentModel} from '../../../../shared/models/register-student.model';
import {RegisterEmployerModel} from '../../../../shared/models/register-employer.model';
import {RegisterService} from '../../../../core/services/register.service';
import {LoginModel} from '../../../../shared/models/login.model';
import {AuthorizationService} from '../../../../core/services/authorization.service';
import {Router} from '@angular/router';

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
  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router,
              private authorizationService: AuthorizationService) {}

  ngOnInit(): void {
    this.generalForm = this.fb.group({
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]],
      role: [this.studentRoleName, [Validators.required]],
      firstName: [null, [Validators.required]],
      middleName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      universityName: [null, [Validators.required]],
      major: [null, [Validators.required]],
      companyName: [null, [Validators.required]],
      companyDescription: [null, [Validators.required]],
      companyAddress: [null, [Validators.required]],
      contactPhoneNumber: [null, [Validators.required]],
      siteUrl: [null, [Validators.required]],
    });

    this.generalForm.get('role').valueChanges.subscribe(value => {
      console.log(this.generalForm.get('role').value);

      if (value == this.studentRoleName && this.radioValue != this.studentRoleName) {
        console.log('value == this.studentRoleName');
        this.radioValue = this.studentRoleName;
        this.generalForm.controls.firstName.setValidators([Validators.required]);
        this.generalForm.controls.middleName.setValidators([Validators.required]);
        this.generalForm.controls.lastName.setValidators([Validators.required]);
        this.generalForm.controls.dateOfBirth.setValidators([Validators.required]);
        this.generalForm.controls.universityName.setValidators([Validators.required]);
        this.generalForm.controls.major.setValidators([Validators.required]);
        this.generalForm.controls.companyName.setValidators(null);
        this.generalForm.controls.companyDescription.setValidators(null);
        this.generalForm.controls.companyAddress.setValidators(null);
        this.generalForm.controls.contactPhoneNumber.setValidators(null);
        this.generalForm.controls.siteUrl.setValidators(null);

        for (const i in this.generalForm.controls) {
          this.generalForm.controls[i].updateValueAndValidity();
        }

      } else if (value == this.employerRoleName && this.radioValue != this.employerRoleName) {
        console.log('value == this.employerRoleName');
        this.radioValue = this.employerRoleName;
        this.generalForm.controls.firstName.setValidators(null);
        this.generalForm.controls.middleName.setValidators(null);
        this.generalForm.controls.lastName.setValidators(null);
        this.generalForm.controls.dateOfBirth.setValidators(null);
        this.generalForm.controls.universityName.setValidators(null);
        this.generalForm.controls.major.setValidators(null);
        this.generalForm.controls.companyName.setValidators([Validators.required]);
        this.generalForm.controls.companyDescription.setValidators([Validators.required]);
        this.generalForm.controls.companyAddress.setValidators([Validators.required]);
        this.generalForm.controls.contactPhoneNumber.setValidators([Validators.required]);
        this.generalForm.controls.siteUrl.setValidators([Validators.required]);

        for (const i in this.generalForm.controls) {
          this.generalForm.controls[i].updateValueAndValidity();
        }
      }
    });
  }

  submitForm(): void {
    // for (const i in this.generalForm.controls) {
    //   this.generalForm.controls[i].markAsDirty();
    //   this.generalForm.controls[i].updateValueAndValidity();
    // }

    console.log('form submit');

    if (this.radioValue == this.studentRoleName){
      this.fillStudentModel();
    } else if (this.radioValue == this.employerRoleName) {
      this.fillEmployerModel();
    }
  }

  fillStudentModel(){
    if (this.generalForm.valid) {
      console.log('fill model');
      let registrationStudentModel: RegisterStudentModel = {
        email: this.generalForm.value.email,
        password: this.generalForm.value.password,
        firstName: this.generalForm.value.firstName,
        middleName: this.generalForm.value.middleName,
        lastName: this.generalForm.value.lastName,
        dateOfBirth: this.generalForm.value.dateOfBirth,
        universityName: this.generalForm.value.universityName,
        major: this.generalForm.value.major
      };
      this.registerStudent(registrationStudentModel);
    }
  }

  fillEmployerModel(){
    if (this.generalForm.valid) {
      console.log('fill model');
      let registrationEmployerModel: RegisterEmployerModel = {
        email: this.generalForm.value.email,
        password: this.generalForm.value.password,
        companyName: this.generalForm.value.companyName,
        companyDescription: this.generalForm.value.companyDescription,
        companyAddress: this.generalForm.value.companyAddress,
        contactPhoneNumber: this.generalForm.value.contactPhoneNumber,
        siteUrl: this.generalForm.value.siteUrl
      };
      this.registerEmployer(registrationEmployerModel);
    }
  }

  registerStudent(registerViewModel: RegisterStudentModel): void {
    this.registerService.registerStudent(registerViewModel).subscribe(
      res => {
        let loginModel = {
          email: registerViewModel.email,
          password: registerViewModel.password
        } as LoginModel;
        this.login(loginModel);
      },
      errors => {
        this.errorMessage = errors.message;
        console.log(this.errorMessage);
      });
  }

  registerEmployer(registerViewModel: RegisterEmployerModel): void {
    this.registerService.registerEmployer(registerViewModel).subscribe(
      res => {
        let loginModel = {
          email: registerViewModel.email,
          password: registerViewModel.password
        } as LoginModel;
        this.login(loginModel);
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

  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.generalForm.controls.checkPassword.updateValueAndValidity());
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
