import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {StudentRoutingModule} from './student-routing.module';
import { StudentProfileComponent } from './student-profile/student-profile.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgZorroAntdModule} from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';

@NgModule({
  declarations: [StudentProfileComponent],
  imports: [
    CommonModule,
    StudentRoutingModule,
    ReactiveFormsModule,
    NgZorroAntdModule,
    FormsModule
  ]
})
export class StudentModule { }
