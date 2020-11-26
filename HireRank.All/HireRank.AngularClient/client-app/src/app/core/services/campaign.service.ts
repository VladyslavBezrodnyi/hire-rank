import { Campaign } from "../../shared/models/campaign.model";
import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import { PagedResult } from "../../shared/models/paged-result.model";
import { GetAdminCampaign } from "../../shared/models/get-admin-campaigns.model";

@Injectable({
    providedIn: 'root'
  })
  export class CampaignService {
    controllerUrl: string = environment.apiUrl + '/campaigns';
  
    constructor(private http: HttpClient) { }

    getById(id: string): Observable<Campaign> {
      return this.http.get<Campaign>(this.controllerUrl + '/' + id);
    }

    getAll(): Observable<Campaign[]> {
      return this.http.get<Campaign[]>(this.controllerUrl);
    }

    getAdminCampaigns(model: GetAdminCampaign): Observable<PagedResult<Campaign>> {
      let params = new HttpParams();

      if (model.name)
        params = params.set("Name", model.name);

      if (model.startDateFrom)
        params = params.set("StartDateFrom", this.getShortDate(model.startDateFrom));
      
      if (model.startDateTo)
        params = params.set("StartDateTo", this.getShortDate(model.startDateTo));

      if (model.endDateFrom)
        params = params.set("EndDateFrom", this.getShortDate(model.endDateFrom));
      
      if (model.endDateTo)
        params = params.set("EndDateTo", this.getShortDate(model.endDateTo));
      
      if (model.pageNumber)
        params = params.set("Paging.PageNumber", model.pageNumber?.toString());
      
      if (model.pageSize)
        params = params.set("Paging.PageSize", model.pageSize?.toString());
      
      if (model.sortingOrder)
        params = params.set("Sorting.SortingOrder", model.sortingOrder?.toString());
      
      if (model.sortingProperty)
        params = params.set("Sorting.SortingProperty", model.sortingProperty?.toString());
      
      return this.http.get<PagedResult<Campaign>>(this.controllerUrl + '/admin', { params: params });
    }

    create(employer: Campaign): Observable<string> {
      return this.http.post<string>(this.controllerUrl, employer);
    }

    edit(employer: Campaign): Observable<string> {
      return this.http.patch<string>(this.controllerUrl, employer);
    }

    delete(id: string): Observable<string> {
      return this.http.delete<string>(this.controllerUrl + "/" + id);
    }

    getShortDate(date: Date): string {
      return date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate();
    }
  }