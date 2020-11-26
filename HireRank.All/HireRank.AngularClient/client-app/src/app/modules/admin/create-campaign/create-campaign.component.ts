import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Campaign } from "../../../shared/models/campaign.model";
import { CampaignService } from "../../../core/services/campaign.service";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-campaign',
  templateUrl: './create-campaign.component.html',
  styleUrls: ['./create-campaign.component.css']
})
export class CreateCampaignComponent implements OnInit {

  createForm: FormGroup;

  @Output() createdEvent = new EventEmitter<boolean>();

  constructor(private campaignService: CampaignService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createForm = this.fb.group({
      name: [null, [Validators.maxLength(60), Validators.required]],
      dates: [null, [Validators.required]]
    });
  }

  submit(): void {
    let campaign = new Campaign();
    campaign.name = this.createForm.value.name;
    campaign.startDate = this.createForm.value.dates[0];
    campaign.endDate = this.createForm.value.dates[this.createForm.value.dates.length - 1];

    this.campaignService.create(campaign).subscribe(result => {
      this.createdEvent.next(true);
    },
    error => {
      this.createdEvent.next(false);
    });
  }

}
