import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Employer} from '../../shared/models/employer.model';
import {Vacancy} from '../../shared/models/vacancy.model';
import {StudentVacancy} from '../../shared/models/student-vacancy.model';

@Injectable({
  providedIn: 'root'
})
export class EmployerService {
  controllerUrl: string = environment.apiUrl + '/employer/';

  constructor(private http: HttpClient) { }

  edit(employer: Employer): Observable<Employer> {
    return this.http.put<Employer>(this.controllerUrl + 'update', employer);
  }

  get(): Observable<Employer> {
    return this.http.get<Employer>(this.controllerUrl + 'profile');
  }

  getVacancyRating(vacancyId: number): Observable<StudentVacancy[]> {
    return this.http.get<StudentVacancy[]>(this.controllerUrl + 'vacancy-rating/' + vacancyId.toString());
  }
}
