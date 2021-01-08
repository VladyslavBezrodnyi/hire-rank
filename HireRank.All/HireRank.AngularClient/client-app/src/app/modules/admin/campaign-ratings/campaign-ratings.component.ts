import { Component, OnInit } from '@angular/core';
import {PagedResult} from '../../../shared/models/paged-result.model';
import {EmployerService} from '../../../core/services/employer.service';
import {Vacancy} from '../../../shared/models/vacancy.model';
import {VacancyService} from '../../../core/services/vacancy.service';
import { ActivatedRoute } from '@angular/router';
import { GetVacancies } from '../../../shared/models/get-vacancies.model';


@Component({
  selector: 'app-campaign-ratings',
  templateUrl: './campaign-ratings.component.html',
  styleUrls: ['./campaign-ratings.component.css']
})
export class CampaignRatingsComponent implements OnInit {

  campaignId: string;
  constructor(private vacancyService: VacancyService, private activateRoute: ActivatedRoute, private employerService: EmployerService) {
    this.campaignId = activateRoute.snapshot.params['id'];
  }

  vacancies: PagedResult<Vacancy> = {
    totalCount: 1,
    items: []
  };

  selectedValue:String;

  ngOnInit(): void {
    let model = new GetVacancies();
    model.campaignIds = [this.campaignId];
    model.pageSize = 100;
    model.pageNumber = 1;

    this.vacancyService.getVacancies(model)
      .subscribe(result => {
        this.vacancies = result;
        console.log(result);
      });
  }

  selectVacancy(id: any) {
    if (id == null || id == '') {
      this.selectedValue = id;
      console.log(this.selectedValue);
    }
  }
}
