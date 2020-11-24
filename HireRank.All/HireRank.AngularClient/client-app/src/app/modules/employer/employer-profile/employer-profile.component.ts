import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Employer} from '../../../shared/models/employer.model';
import {EmployerService} from '../../../core/services/employer.service';

@Component({
  selector: 'app-employer-profile',
  templateUrl: './employer-profile.component.html',
  styleUrls: ['./employer-profile.component.css']
})
export class EmployerProfileComponent implements OnInit {
  employer: Employer = new Employer();
  employerForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private employerService: EmployerService) {
  }

  ngOnInit() {
    this.getEmployer();

    this.employerForm = this.formBuilder.group({
      companyName: [null, [Validators.required]],
      companyDescription: [null, [Validators.required]],
      companyAddress: [null, [Validators.required]],
      contactPhoneNumber : [null, [Validators.required]],
      siteUrl : [null, [Validators.required]],
    });
  }

  getEmployer() {
    // ЗАГЛУШКА
    this.employer = { companyName: 'NIX Solutions', companyDescription: 'IT компанія', companyAddress: 'Харків',
      contactPhoneNumber: '+380123456789', siteUrl: 'nix.com' };

    // this.employerService.get()
    //   .subscribe(data => {
    //     this.employer = data;
    //   });
  }

  submitEmployerForm() {
    // this.employerService.edit(this.employer)
    //   .subscribe(data => {
    //     this.employer = data;
    //   });
  }
}
