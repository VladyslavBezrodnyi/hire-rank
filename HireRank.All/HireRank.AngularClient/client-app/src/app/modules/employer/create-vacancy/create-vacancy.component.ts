import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CampaignService } from 'src/app/core/services/campaign.service';
import { VacancyService } from 'src/app/core/services/vacancy.service';
import { Campaign } from 'src/app/shared/models/campaign.model';
import { EmployerVacancy } from 'src/app/shared/models/employer-vacancy.model';

@Component({
  selector: 'app-create-vacancy',
  templateUrl: './create-vacancy.component.html',
  styleUrls: ['./create-vacancy.component.css']
})
export class CreateVacancyComponent implements OnInit {

  createForm: FormGroup;
  activeCampaings: Array<Campaign>;
  testQuestionWeight: string = "100";

  @Output() createdEvent = new EventEmitter<boolean>();

  constructor(private vacancyService: VacancyService,
              private campaignService: CampaignService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createForm = this.fb.group({
      title: [null, [Validators.maxLength(200), Validators.required]],
      description: [null, [Validators.required, Validators.maxLength(2000), Validators.minLength(50)]],
      testSize: [1, [Validators.required]],
      campaignId: [null, [Validators.required]]
    });

    this.getActiveCampaigns();

    this.createForm.get("testSize").valueChanges.subscribe(x => {
      this.testQuestionWeight = (100 / x).toPrecision(3);
    });
  }

  getActiveCampaigns(): void {
    this.campaignService.getActive().subscribe(x => {
      this.activeCampaings = x;
    });
  }

  submitForm(): void {
    debugger
    let vacancy = new EmployerVacancy();
    vacancy.title = this.createForm.value.title;
    vacancy.description = this.createForm.value.description;
    vacancy.testSize = this.createForm.value.testSize;
    vacancy.campaignId = this.createForm.value.campaignId;

    this.vacancyService.create(vacancy).subscribe(result => {
      this.createdEvent.next(true);
    },
    error => {
      this.createdEvent.next(false);
    });
  }

}
