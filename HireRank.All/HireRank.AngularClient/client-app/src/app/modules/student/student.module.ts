import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {StudentRoutingModule} from './student-routing.module';
import { StudentProfileComponent } from './student-profile/student-profile.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgZorroAntdModule} from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import { StudentVacanciesComponent } from './student-vacancies/student-vacancies.component';
import {DragDropModule} from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [StudentProfileComponent, StudentVacanciesComponent],
  imports: [
    CommonModule,
    StudentRoutingModule,
    ReactiveFormsModule,
    NgZorroAntdModule,
    FormsModule,
    DragDropModule
  ]
})
export class StudentModule { }
