import { ConfirmationEmployer } from './../../shared/models/confirmation-employer.model';
import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Admin} from '../../shared/models/admin.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  controllerUrl: string = environment.apiUrl + '/admin';

  constructor(private http: HttpClient) { }

  edit(admin: Admin): Observable<Admin> {
    return this.http.put<Admin>(this.controllerUrl + '', admin);
  }

  get(): Observable<Admin> {
    return this.http.get<Admin>(this.controllerUrl + '');
  }

  getEmployersForConfirmation(): Observable<ConfirmationEmployer[]> {
    return this.http.get<ConfirmationEmployer[]>(this.controllerUrl + '/allEmployer');
  }

  getNonConfirmedEmployers(): Observable<ConfirmationEmployer[]> {
    return this.http.get<ConfirmationEmployer[]>(this.controllerUrl + '/nonConfirmedEmployer');
  }

  confirmEmployer(employerId: string): Observable<boolean> {
    return this.http.post<boolean>(this.controllerUrl + '/confirm', { id: employerId});
  }
}
