import { Component, OnInit } from '@angular/core';
import { PagedResult } from 'src/app/shared/models/paged-result.model';
import { EmployerVacancy } from 'src/app/shared/models/employer-vacancy.model';
import { Vacancy } from 'src/app/shared/models/vacancy.model';
import { GetEmployerVacancies} from "../../../shared/models/get-employer-vacancies.model";
import { VacancyService } from "../../../core/services/vacancy.service";
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { CreateVacancyComponent } from '../create-vacancy/create-vacancy.component';
import { CampaignService } from 'src/app/core/services/campaign.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employer-vacancies',
  templateUrl: './employer-vacancies.component.html',
  styleUrls: ['./employer-vacancies.component.css']
})
export class EmployerVacanciesComponent implements OnInit {

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

  constructor(
    private vacancyService: VacancyService,
    private campaignService: CampaignService,
    private messageService: NzMessageService,
    private router: Router) { }


  ngOnInit(): void {
    this.getCampaignFilters();
    this.queryFilter.sortingProperty = "DateCreated";
    this.queryFilter.sortingOrder = "desc";
    this.queryFilter.pageSize = 2;
    this.queryFilter.pageNumber = 1;
  }

  loadVacancies(): void {
    this.loading = true;
    this.vacancyService
      .getEmployerVacancies(this.queryFilter)
      .subscribe(result => {
        this.vacancies =  result;
        this.loading = false;
      });
  }

  openCreatingForm() {
    this.isCreatingModalVisible = true;
  }

  hideCreatingForm() {
    this.isCreatingModalVisible = false;
  }

  openEditingForm(id: string) {
    this.vacancyService.getById(id).subscribe(x => {
      this.selectedVacancy = new EmployerVacancy();
      this.selectedVacancy.id = x.id;
      this.selectedVacancy.title = x.title;
      this.selectedVacancy.description = x.description;
      this.selectedVacancy.campaignId = x.campaign.id;
      this.selectedVacancy.testSize = x.testSize;
      this.isEditingFormVisible = true;
    });
  }

  hideEditingForm() {
    this.isEditingFormVisible = false;
  }

  goToTestEditing(id: string) {
    this.router.navigateByUrl(`employer/vacancy/${id}/tests`);
  }

  getCampaignFilters() {
    var that = this;
    this.campaignService.getActive().subscribe(x => {
      x.forEach((element, index) => {
        that.campaignIdOptions.push({ label: element.name, value: element.id, checked: false });
    });
  });
}

onQueryParamsChange(params: NzTableQueryParams): void {
  const { pageSize, pageIndex, sort, filter } = params;
  const currentSort = sort.find(item => item.value !== null);
  const sortField = (currentSort && currentSort.key) || null;
  const sortOrder = (currentSort && currentSort.value) || null;

  this.queryFilter.pageNumber =  pageIndex;
  this.queryFilter.pageSize = pageSize;
  this.queryFilter.sortingProperty = sortField;
  this.queryFilter.sortingOrder = ( sortOrder == "ascend") ? "asc" : "desc";
  this.loadVacancies();
}

searchByTitle() {
  this.queryFilter.title = this.titleFilter;
  this.titleFilterVisible = false;
  this.loadVacancies();
}

resetFilterByTitle() {
  this.titleFilter = '';
  this.queryFilter.title = this.titleFilter;
  this.titleFilterVisible = false;
  this.loadVacancies();
}

searchByCampaign() {
  this.campiagnIdFilters = [];
  this.campaignIdOptions.forEach(option => {
    if (option.checked){
      this.campiagnIdFilters.push(option.value);
    }
  })
  this.queryFilter.pageNumber = 1;
  this.queryFilter.campaignIds = this.campiagnIdFilters;
  this.campiagnIdFilterVisible = false;
  this.loadVacancies();
}

resetFilterByCampaign() {
  this.campaignIdOptions.forEach(element => {
    element.checked = false
  })
}

deleteVacancy(id: string) {
  this.vacancyService.delete(id).subscribe(result => {
    this.messageService.success("Ви успішно видалили вакансію!");
    this.loadVacancies();
  },
  error => {
    this.messageService.error("Трапилась помилка при видаленні вакансії!");
  });
}

afterVacancyCreating(value: boolean) {
  if (value) {
    this.messageService.success("Ви успішно створили вакансію!");
  }
  else {
    this.messageService.error("Трапилась помилка при створенні вакансії!");
  }
  this.hideCreatingForm();
  this.loadVacancies();
}

afterVacancyEditing(value: boolean) {
  if (value) {
    this.messageService.success("Ви успішно відредагували вакансію!");
  }
  else {
    this.messageService.error("Трапилась помилка при редагуванні вакансії!");
  }
  this.hideEditingForm();
  this.loadVacancies();
}

}
