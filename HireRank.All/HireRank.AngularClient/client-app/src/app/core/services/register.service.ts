import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegisterStudentModel} from '../../shared/models/register-student.model';
import {RegisterEmployerModel} from '../../shared/models/register-employer.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  controllerUrl: string = environment.apiUrl + '/account/register/';

  constructor(private http: HttpClient) { }

  registerStudent(registerViewModel: RegisterStudentModel): Observable<any> {
    return this.http.post(this.controllerUrl + 'student', registerViewModel);
  }

  registerEmployer(registerViewModel: RegisterEmployerModel): Observable<any> {
    return this.http.post(this.controllerUrl + 'employer', registerViewModel);
  }
}
