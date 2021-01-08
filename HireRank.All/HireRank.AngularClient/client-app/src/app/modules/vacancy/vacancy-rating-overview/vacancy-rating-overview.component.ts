import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { VacancyService} from '../../../core/services/vacancy.service';
import { CampaignProcessingStates } from '../../../shared/models/campaign-state.model';
import { Vacancy } from '../../../shared/models/vacancy.model';
import { StudentVacancy } from '../../../shared/models/student-vacancy.model';
import { CampaignService } from '../../../core/services/campaign.service';

@Component({
  selector: 'app-vacancy-rating-overview',
  templateUrl: './vacancy-rating-overview.component.html',
  styleUrls: ['./vacancy-rating-overview.component.css']
})
export class VacancyRatingOverviewComponent implements OnInit {

  loading: boolean = false;
  @Input('vacancyId') vacancyId: string;

  campaignStatus: CampaignProcessingStates = null;

  vacancy: Vacancy;

  vacancyRatings: StudentVacancy[] = [];

  constructor(private vacancyService: VacancyService, private campaignService: CampaignService) { }

  ngOnChanges(changes: SimpleChanges) {
    if(this.vacancyId){
      this.loadVacancyWithRating();
    }
  }

  ngOnInit(): void {
  }

  loadVacancyWithRating() {
    this.loading = true;
    this.vacancyService.getById(this.vacancyId)
      .subscribe(x => {
        this.vacancy = x;
        this.campaignService.getState(this.vacancy?.campaign?.id).subscribe(x => this.campaignStatus = x);
      });

    this.vacancyService.getVacancyRating(this.vacancyId)
    .subscribe(x => {
      this.vacancyRatings = x;
      this.loading = false;
    });
  }
}