import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Test} from '../../shared/models/test.model';
import {TestResult} from '../../shared/models/test-result.model';

@Injectable({
  providedIn: 'root'
})
export class TestingService {
  controllerUrl: string = environment.apiUrl + '/testing/';

  constructor(private http: HttpClient) { }

  getTestByVacancyId(vacancyId: string): Observable<Test> {
    return this.http.get<Test>(this.controllerUrl + vacancyId);
  }

  addTestResult(testResult: TestResult): Observable<number> {
    return this.http.post<number>(this.controllerUrl + 'addTestResult', testResult);
  }
}
