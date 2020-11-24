import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AdminService} from '../../../core/services/admin.service';
import {Admin} from '../../../shared/models/admin.model';

@Component({
  selector: 'app-admin-profile',
  templateUrl: './admin-profile.component.html',
  styleUrls: ['./admin-profile.component.css']
})
export class AdminProfileComponent implements OnInit {
  admin: Admin = new Admin();
  adminForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private adminService: AdminService) {
  }

  ngOnInit() {
    this.getAdmin();

    this.adminForm = this.formBuilder.group({
      name: [null, [Validators.required]],
      contactPhone : [null, [Validators.required]],
    });
  }

  getAdmin() {
    // ЗАГЛУШКА
    this.admin = { name: 'Адміністратор Олег', contactPhone: '+380987654321' };

    // this.adminService.get()
    //   .subscribe(data => {
    //     this.admin = data;
    //   });
  }

  submitAdminForm() {
    // this.adminService.edit(this.admin)
    //   .subscribe(data => {
    //     this.admin = data;
    //   });
  }
}
