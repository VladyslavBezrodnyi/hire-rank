import { AdminService } from './../../../core/services/admin.service';
import { ConfirmationEmployer } from '../../../shared/models/confirmation-employer.model';
import { Component, OnInit } from '@angular/core';
import { NzTableLayout, NzTablePaginationPosition, NzTableQueryParams, NzTableSize } from 'ng-zorro-antd/table';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-employer-confirmation',
  templateUrl: './employer-confirmation.component.html',
  styleUrls: ['./employer-confirmation.component.css']
})
export class EmployerConfirmationComponent implements OnInit {

  employers: Array<ConfirmationEmployer> = [];

  constructor(private adminService: AdminService,
    private messageService: NzMessageService) { }

  ngOnInit(): void {
    this.loadConfirmationEmployer();
  }

  loadConfirmationEmployer(): void {
    this.adminService
      .getEmployersForConfirmation()
      .subscribe(result => {
        this.employers =  result;
      });
  }

  confirmEmployer(сonfirmationEmployer: ConfirmationEmployer): void {
    this.adminService
      .confirmEmployer(сonfirmationEmployer.id)
      .subscribe(result => {
        сonfirmationEmployer.isConfirmed = result;
      });
  }
}
