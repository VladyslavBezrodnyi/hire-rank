import { Component, OnInit } from '@angular/core';
import {StudentVacancy} from '../../../shared/models/student-vacancy.model';
import {Vacancy} from '../../../shared/models/vacancy.model';
import {VacancyService} from '../../../core/services/vacancy.service';
import {GetEmployerVacancies} from '../../../shared/models/get-employer-vacancies.model';
import {PagedResult} from '../../../shared/models/paged-result.model';
import {EmployerService} from '../../../core/services/employer.service';

@Component({
  selector: 'app-vacancy-rating',
  templateUrl: './vacancy-rating.component.html',
  styleUrls: ['./vacancy-rating.component.css']
})
export class VacancyRatingComponent implements OnInit {
  studentVacancies: StudentVacancy[] = [];
  queryFilter: GetEmployerVacancies = new GetEmployerVacancies();

  vacancies: PagedResult<Vacancy> = {
    totalCount: 1,
    items: []
  };

  selectedValue = null;

  constructor(private vacancyService: VacancyService, private employerService: EmployerService) { }

  ngOnInit(): void {
    this.queryFilter.pageSize = 100;
    this.queryFilter.pageNumber = 1;

    this.vacancyService.getEmployerVacancies(this.queryFilter)
      .subscribe(result => {
        this.vacancies = result;
      });
  }

  selectVacancy(id: any) {
    if (id == null || id == '') {
      return
    }

    this.employerService.getVacancyRating(id as number)
      .subscribe(result => {
        this.studentVacancies = result;
        this.studentVacancies.sort((f, s) => s.score - f.score);
      });
  }

  getDateOfBirth(dateTime: Date) {
    // return dateTime.getDate().toString() + '.' + (dateTime.getMonth() + 1).toString() + '.' + dateTime.getFullYear()
    return dateTime.toString().substr(0, 10);
  }
}
