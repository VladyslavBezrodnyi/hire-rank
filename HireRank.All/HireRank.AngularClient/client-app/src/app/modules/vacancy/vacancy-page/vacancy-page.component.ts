import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {VacancyService} from '../../../core/services/vacancy.service';
import {Vacancy} from '../../../shared/models/vacancy.model';
import {Employer} from '../../../shared/models/employer.model';
import {Campaign} from '../../../shared/models/campaign.model';

@Component({
  selector: 'app-vacancy-page',
  templateUrl: './vacancy-page.component.html',
  styleUrls: ['./vacancy-page.component.css']
})
export class VacancyPageComponent implements OnInit {
  id: string;
  vacancy: Vacancy = new Vacancy();
  loading = true;

  constructor(private activateRoute: ActivatedRoute, private vacancyService: VacancyService, private router: Router) {
    this.loading = true;
    this.vacancy.employer = new Employer();
    this.vacancy.campaign = new Campaign();

    this.id = activateRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.vacancyService.getById(this.id)
      .subscribe(data => {
        this.vacancy = data;
        this.loading = false;
      });
  }

  startTesting() {
    this.router.navigate(['/vacancy/test', this.id]);
  }
}
