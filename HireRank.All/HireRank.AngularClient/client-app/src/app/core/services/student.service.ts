import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs';
import {Student} from '../../shared/models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  controllerUrl: string = environment.apiUrl + '/student/';

  constructor(private http: HttpClient) { }

  edit(student: Student): Observable<Student> {
    return this.http.put<Student>(this.controllerUrl + 'update', student);
  }

  get(): Observable<Student> {
    return this.http.get<Student>(this.controllerUrl + 'profile');
  }
}
