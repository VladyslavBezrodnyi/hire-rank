import { Component, OnInit } from '@angular/core';
import { PagedResult } from 'src/app/shared/models/paged-result.model';
import { EmployerVacancy } from 'src/app/shared/models/employer-vacancy.model';
import { Vacancy } from 'src/app/shared/models/vacancy.model';
import { GetEmployerVacancies} from "../../../shared/models/get-employer-vacancies.model";
import { VacancyService } from "../../../core/services/vacancy.service";
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { CampaignService } from 'src/app/core/services/campaign.service';

@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrls: ['./vacancy-list.component.css']
})
export class VacancyListComponent implements OnInit {

  isCreatingModalVisible: boolean = false;
  selectedVacancy: EmployerVacancy;

  vacancies: PagedResult<any> = {
    totalCount: 1,
    items: []
  };

  loading: Boolean = false;
  queryFilter: GetEmployerVacancies = new GetEmployerVacancies();
  totalСount: 1;
  titleFilter: string = "";
  campiagnIdFilters: string[] = [];
  titleFilterVisible: boolean = false;
  campiagnIdFilterVisible: false;
  isEditingFormVisible: boolean = false;
  campaignIdOptions: Array<{ label: string; value: string,  checked: boolean }> = [];

  constructor(private vacancyService: VacancyService) { }


  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
