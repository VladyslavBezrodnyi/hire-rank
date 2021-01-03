import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {VacancyService} from '../../../core/services/vacancy.service';
import {Vacancy} from '../../../shared/models/vacancy.model';
import {Employer} from '../../../shared/models/employer.model';
import {Campaign} from '../../../shared/models/campaign.model';
import {AuthorizationService} from '../../../core/services/authorization.service';
import {TestingService} from '../../../core/services/testing.service';
import {Test} from '../../../shared/models/test.model';

@Component({
  selector: 'app-vacancy-page',
  templateUrl: './vacancy-page.component.html',
  styleUrls: ['./vacancy-page.component.css']
})
export class VacancyPageComponent implements OnInit {
  id: string;
  vacancy: Vacancy = new Vacancy();
  loading = true;
  startTestingButtonName = '';
  isAuthorizedStudent: boolean;
  isTestAlreadyPassed: boolean;

  constructor(private activateRoute: ActivatedRoute, private vacancyService: VacancyService, private router: Router,
              private authorizationService: AuthorizationService, private testingService: TestingService) {
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

    this.isAuthorizedStudent = this.authorizationService.currentUser != null && this.authorizationService.currentUser.role == 'student';
    if (!this.isAuthorizedStudent) {
      this.startTestingButtonName = 'Авторизуйтесь як студент, щоб пройти тест';
      return;
    }

    this.testingService.getTestByVacancyId(this.id)
      .subscribe(data => {
        this.isTestAlreadyPassed = data.isPassed;
        this.startTestingButtonName = this.isTestAlreadyPassed ? 'Ви вже пройшли тест' : 'Розпочати тестування';
      });
  }

  startTesting() {
    this.router.navigate(['/vacancy/test', this.id]);
  }
}
