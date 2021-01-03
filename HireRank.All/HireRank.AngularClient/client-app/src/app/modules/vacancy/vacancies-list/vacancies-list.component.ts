import { Component, OnInit } from '@angular/core';
import {Vacancy} from '../../../shared/models/vacancy.model';
import {VacancyService} from '../../../core/services/vacancy.service';
import {PagedResult} from '../../../shared/models/paged-result.model';
import {GetVacancies} from '../../../shared/models/get-vacancies.model';
import {Router} from '@angular/router';
import {AuthorizationService} from '../../../core/services/authorization.service';

@Component({
  selector: 'app-vacancies-list',
  templateUrl: './vacancies-list.component.html',
  styleUrls: ['./vacancies-list.component.css']
})
export class VacanciesListComponent implements OnInit {
  queryFilter: GetVacancies = new GetVacancies();

  vacancies: PagedResult<Vacancy> = {
    totalCount: 1,
    items: []
  };

  constructor(private vacancyService: VacancyService, private router: Router, private authorizationService: AuthorizationService) { }

  ngOnInit(): void {
    this.queryFilter.pageSize = 1;
    this.queryFilter.pageNumber = 1;
    this.getVacancies();
  }

  getVacancies() {
    this.vacancyService.getVacancies(this.queryFilter)
      .subscribe(result => {
        this.vacancies = result;
      });
  }

  selectVacancy(id: string) {
    this.router.navigate(['/vacancy', id]);
  }

  searchByTitle(title: string) {
    this.queryFilter.title = title;
    this.getVacancies();
  }

  changeFilter() {
    this.getVacancies();
  }
}
