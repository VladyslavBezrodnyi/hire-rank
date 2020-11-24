import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Employer} from '../../shared/models/employer.model';

@Injectable({
  providedIn: 'root'
})
export class EmployerService {
  controllerUrl: string = environment.apiUrl + '';

  constructor(private http: HttpClient) { }

  edit(employer: Employer): Observable<Employer> {
    return this.http.put<Employer>(this.controllerUrl + '', employer);
  }

  get(): Observable<Employer> {
    return this.http.get<Employer>(this.controllerUrl + '');
  }
}
