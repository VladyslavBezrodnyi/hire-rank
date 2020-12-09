import { GetEmployerQuestion } from "../../shared/models/get-employer-question.model";
import { Question } from "../../shared/models/question.model";
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from "../../shared/models/paged-result.model";

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  controllerUrl: string = environment.apiUrl + '/questions';

  constructor(private http: HttpClient) { }

  getTags(): Observable<string[]> {
    return this.http.get<string[]>(this.controllerUrl + "/tags");
  }

  getEmployerQuestions(model: GetEmployerQuestion): Observable<PagedResult<any>> {
    let params = new HttpParams();

    if (model.text)
      params = params.append("Text", model.text);

    if (model.tags && model.tags.length > 0) {
      model.tags.forEach((tag) => {
        params = params.append(`Tags`, tag);
      });
    }

    if (model.pageNumber)
      params = params.append("Paging.PageNumber", model.pageNumber?.toString());

    if (model.pageSize)
      params = params.append("Paging.PageSize", model.pageSize?.toString());

    if (model.sortingOrder)
      params = params.append("Sorting.SortingOrder", model.sortingOrder?.toString());

    if (model.sortingProperty)
      params = params.append("Sorting.SortingProperty", model.sortingProperty?.toString());

    return this.http.get<PagedResult<any>>(this.controllerUrl, { params: params });
  }

  getById(id: string): Observable<Question> {
    return this.http.get<Question>(this.controllerUrl + "/" + id);
  }

  create(question: Question): Observable<string> {
    return this.http.post<string>(this.controllerUrl, question);
  }

  edit(question: Question): Observable<string> {
    return this.http.put<string>(this.controllerUrl, question);
  }

  delete(id: string): Observable<string> {
    return this.http.delete<string>(this.controllerUrl + "/" + id);
  }
}