import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CampaignService } from 'src/app/core/services/campaign.service';
import { Campaign } from 'src/app/shared/models/campaign.model';

@Component({
  selector: 'app-edit-campaign',
  templateUrl: './edit-campaign.component.html',
  styleUrls: ['./edit-campaign.component.css']
})
export class EditCampaignComponent implements OnInit {
  editForm: FormGroup;

  @Input() campaign: Campaign;
  @Output() editedEvent = new EventEmitter<boolean>();

  constructor(private campaignService: CampaignService, private fb: FormBuilder) { }

  ngOnChanges(changes: SimpleChanges) {
    if (this.editForm) {
      this.editForm.controls["name"].setValue(this.campaign?.name);
      let dates = [];
      dates.push(this.campaign?.startDate);
      dates.push(this.campaign?.endDate);
      this.editForm.controls["dates"].setValue(dates);
    }
  }

  ngOnInit(): void {
    this.editForm = this.fb.group({
      name: [null, [Validators.maxLength(60), Validators.required]],
      dates: [null, [Validators.required]]
    });
  }

  submit(): void {
    let editedCampaign = new Campaign();
    
    editedCampaign.name = this.editForm.value.name;
    editedCampaign.startDate = this.editForm.value.dates[0];
    editedCampaign.endDate = this.editForm.value.dates[this.editForm.value.dates.length - 1];
    editedCampaign.id = this.campaign.id;
    this.campaignService.edit(editedCampaign).subscribe(result => {
      this.editedEvent.next(true);
    },
      error => {
        this.editedEvent.next(false);
      });
  }

}
