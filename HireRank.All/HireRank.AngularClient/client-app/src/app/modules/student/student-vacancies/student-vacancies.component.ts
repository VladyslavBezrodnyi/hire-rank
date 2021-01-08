import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import {Vacancy} from '../../../shared/models/vacancy.model';
import {VacancyService} from '../../../core/services/vacancy.service';
import {AuthorizationService} from '../../../core/services/authorization.service';
import {AssignVacancyPriorityModel} from '../../../shared/models/assign-vacancy-priority.model';

@Component({
  selector: 'app-student-vacancies',
  templateUrl: './student-vacancies.component.html',
  styleUrls: ['./student-vacancies.component.css']
})
export class StudentVacanciesComponent implements OnInit {

  vacancies: Vacancy[];
  result: { [campaignName: string]: Vacancy[]; } = { };
  campaigns: string[] = [];
  dirtyCampaigns = new Set();
  areAnyVacancies = true;

  constructor(private vacancyService: VacancyService, private authorizationService: AuthorizationService) { }

  ngOnInit(): void {
    this.getAllVacancies();
  }

  drop(campaignName: string, event: CdkDragDrop<string[]>): void {
    moveItemInArray(this.result[campaignName], event.previousIndex, event.currentIndex);
    this.dirtyCampaigns.add(campaignName);
  }

  getAllVacancies() {
    this.vacancyService.getAllStudentVacancies(this.authorizationService?.currentUser?.id)
      .subscribe(data => {
        this.vacancies = data;

        if (this.vacancies == null || this.vacancies.length < 1) {
          this.areAnyVacancies = false;
          return;
        }

        this.result = this.vacancies.reduce((r, a) => {
          r[a.campaign.name] = r[a.campaign.name] || [];
          r[a.campaign.name].push(a);
          return r;
        }, Object.create(null));

        for (let i of Object.keys(this.result)) {
          this.campaigns.push(i);
        }
      });
  }

  assignPriority() {
    for (let i of this.dirtyCampaigns) {
      for (let j = 0; j < (this.result[i.toString()] as Vacancy[]).length; j++) {
        let assignVacancyPriority = {
          studentId: this.authorizationService?.currentUser?.id.toString(),
          vacancyId: this.result[i.toString()][j].id,
          priority: j + 1
        } as AssignVacancyPriorityModel;

        this.vacancyService.assignPriority(assignVacancyPriority)
          .subscribe(data => {
            this.dirtyCampaigns.clear();
          });
      }
    }
  }
}
