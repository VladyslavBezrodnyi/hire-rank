import { Campaign } from "../../shared/models/campaign.model";
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from "../../shared/models/paged-result.model";
import { Vacancy } from "../../shared/models/vacancy.model";
import { GetEmployerVacancies } from "../../shared/models/get-employer-vacancies.model";
import { GetVacancies } from "../../shared/models/get-vacancies.model";
import { EmployerVacancy } from "../../shared/models/employer-vacancy.model";
import { AssignVacancyPriorityModel } from 'src/app/shared/models/assign-vacancy-priority.model';
import { VacancyAvailableQuestion } from "src/app/shared/models/vacancy-available-question.model";

@Injectable({
    providedIn: 'root'
})
export class VacancyService {
    controllerUrl: string = environment.apiUrl + '/vacancies';

    constructor(private http: HttpClient) { }

    getById(id: string): Observable<Vacancy> {
        return this.http.get<Vacancy>(this.controllerUrl + '/' + id);
    }

    getEmployerVacancies(model: GetEmployerVacancies): Observable<PagedResult<Vacancy>> {
        let params = new HttpParams();

        if (model.title)
            params = params.set("Title", model.title);

        if (model.campaignIds && model.campaignIds.length > 0) {
            model.campaignIds.forEach((campaignId) => {
                params = params.append(`CampaignIds`, campaignId);
            });
        }

        if (model.pageNumber)
            params = params.set("Paging.PageNumber", model.pageNumber?.toString());

        if (model.pageSize)
            params = params.set("Paging.PageSize", model.pageSize?.toString());

        if (model.sortingOrder)
            params = params.set("Sorting.SortingOrder", model.sortingOrder?.toString());

        if (model.sortingProperty)
            params = params.set("Sorting.SortingProperty", model.sortingProperty?.toString());

        return this.http.get<PagedResult<Vacancy>>(this.controllerUrl + '/employer', { params: params });
    }

    getVacancies(model: GetVacancies): Observable<PagedResult<Vacancy>> {
        let params = new HttpParams();

        if (model.title)
            params = params.set("Title", model.title);

            if (model.employerCompany)
            params = params.set("EmployerCompany", model.employerCompany);

        if (model.campaignIds && model.campaignIds.length > 0) {
            model.campaignIds.forEach((campaignId) => {
                params = params.append(`CampaignIds`, campaignId);
            });
        }

        if (model.pageNumber)
            params = params.set("Paging.PageNumber", model.pageNumber?.toString());

        if (model.pageSize)
            params = params.set("Paging.PageSize", model.pageSize?.toString());

        if (model.sortingOrder)
            params = params.set("Sorting.SortingOrder", model.sortingOrder?.toString());

        if (model.sortingProperty)
            params = params.set("Sorting.SortingProperty", model.sortingProperty?.toString());

        return this.http.get<PagedResult<Vacancy>>(this.controllerUrl, { params: params });
    }

    getActive(): Observable<Campaign[]> {
        return this.http.get<Campaign[]>(this.controllerUrl + '/active');
    }

    create(vacancy: EmployerVacancy): Observable<string> {
        return this.http.post<string>(this.controllerUrl, vacancy);
    }

    edit(vacancy: EmployerVacancy): Observable<string> {
        return this.http.put<string>(this.controllerUrl, vacancy);
    }

    delete(id: string): Observable<string> {
        return this.http.delete<string>(this.controllerUrl + "/" + id);
    }

    assignPriority(assignVacancyPriorityModel: AssignVacancyPriorityModel): Observable<string> {
        return this.http.post<string>(this.controllerUrl + '/assign-priority', assignVacancyPriorityModel);
    }

    getAllStudentVacancies(studentId: number): Observable<Vacancy[]> {
        return this.http.get<Vacancy[]>(this.controllerUrl + '/student/' + studentId.toString());
    }

    getAvailableVacancyQuestions(vacancyId: string): Observable<VacancyAvailableQuestion[]> {
        return this.http.get<VacancyAvailableQuestion[]>(this.controllerUrl + '/' + vacancyId + '/available-questions')
    }

    addTestsToVacancy(vacancyId: string, questionIds: string[]): Observable<string> {
        return this.http.post<string>(this.controllerUrl + '/' + vacancyId + '/tests', questionIds);
    }

    // getAllStudentVacancies(studentId: number): Observable<Vacancy[]> {
    //   return this.http.get<Vacancy[]>(this.controllerUrl + '/student/' + studentId.toString());
    // }
}
