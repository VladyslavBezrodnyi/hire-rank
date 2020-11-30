import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Vacancy} from '../../shared/models/vacancy.model';
import {AssignVacancyPriorityModel} from '../../shared/models/assign-vacancy-priority.model';

@Injectable({
  providedIn: 'root'
})
export class VacancyService {
  controllerUrl: string = environment.apiUrl + '/vacancies/';

  constructor(private http: HttpClient) { }

  assignPriority(assignVacancyPriorityModel: AssignVacancyPriorityModel): Observable<string> {
    return this.http.post<string>(this.controllerUrl + 'assign-priority', assignVacancyPriorityModel);
  }

  getAllStudentVacancies(studentId: number): Observable<Vacancy[]> {
    return this.http.get<Vacancy[]>(this.controllerUrl + 'student/' + studentId.toString());
  }
}
