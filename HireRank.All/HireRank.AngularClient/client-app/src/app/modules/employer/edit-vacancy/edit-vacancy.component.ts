import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CampaignService } from 'src/app/core/services/campaign.service';
import { VacancyService } from 'src/app/core/services/vacancy.service';
import { Campaign } from 'src/app/shared/models/campaign.model';
import { EmployerVacancy } from 'src/app/shared/models/employer-vacancy.model';
import { Vacancy } from 'src/app/shared/models/vacancy.model';

@Component({
  selector: 'app-edit-vacancy',
  templateUrl: './edit-vacancy.component.html',
  styleUrls: ['./edit-vacancy.component.css']
})
export class EditVacancyComponent implements OnInit {

  editForm: FormGroup;
  activeCampaings: Array<Campaign>;
  testQuestionWeight: string = "100";
  @Input() vacancy: EmployerVacancy;
  @Output() editedEvent = new EventEmitter<boolean>();

  constructor(private vacancyService: VacancyService,
              private campaignService: CampaignService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.editForm = this.fb.group({
      title: [null, [Validators.maxLength(200), Validators.required]],
      description: [null, [Validators.required, Validators.maxLength(2000), Validators.minLength(50)]],
      testSize: [1, [Validators.required]],
      campaignId: [null, [Validators.required]]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.vacancy) {
      console.log(this.vacancy);
      this.editForm = this.fb.group({
        title: [this.vacancy.title, [Validators.maxLength(200), Validators.required]],
        description: [this.vacancy.description, [Validators.required, Validators.minLength(2000), Validators.maxLength(50)]],
        testSize: [this.vacancy.testSize, [Validators.required]],
        campaignId: [this.vacancy.campaignId, [Validators.required]]
      });
  
      this.getActiveCampaigns();
  
      this.editForm.get("testSize").valueChanges.subscribe(x => {
        this.testQuestionWeight = (100 / x).toPrecision(3);
      });
    }
  }

  getActiveCampaigns(): void {
    this.campaignService.getActive().subscribe(x => {
      this.activeCampaings = x;
    });
  }

  submitForm(): void {
    let vacancy = new EmployerVacancy();
    vacancy.id =  this.vacancy.id;
    vacancy.title = this.editForm.value.title;
    vacancy.description = this.editForm.value.description;
    vacancy.testSize = this.editForm.value.testSize;
    vacancy.campaignId = this.editForm.value.campaignId;

    this.vacancyService.edit(vacancy).subscribe(result => {
      this.editedEvent.next(true);
    },
    error => {
      this.editedEvent.next(false);
    });
  }

}
