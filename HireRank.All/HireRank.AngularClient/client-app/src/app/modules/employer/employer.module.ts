import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EmployerRoutingModule} from './employer-routing.module';
import {NgZorroAntdModule} from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import { EmployerProfileComponent } from './employer-profile/employer-profile.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { CreateTestQuestionComponent } from './create-test-question/create-test-question.component';
import { EditTestQuestionComponent } from './edit-test-question/edit-test-question.component';
import { TestQuestionsComponent } from './test-questions/test-questions.component';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { IconsProviderModule } from 'src/app/icons-provider.module';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { EmployerVacanciesComponent } from './employer-vacancies/employer-vacancies.component';
import { CreateVacancyComponent } from './create-vacancy/create-vacancy.component';
import { EditVacancyComponent } from './edit-vacancy/edit-vacancy.component';
import { VacancyRatingComponent } from './vacancy-rating/vacancy-rating.component';
import { NzRadioModule } from 'ng-zorro-antd/radio';

@NgModule({
  declarations: [
    EmployerProfileComponent,
    CreateTestQuestionComponent,
    EditTestQuestionComponent,
    TestQuestionsComponent,
    EmployerVacanciesComponent,
    CreateVacancyComponent,
    EditVacancyComponent,
    VacancyRatingComponent
  ],
  imports: [
    CommonModule,
    NgZorroAntdModule,
    EmployerRoutingModule,
    ReactiveFormsModule,
    NzFormModule,
    NzCheckboxModule,
    NzTableModule,
    NzDropDownModule,
    IconsProviderModule,
    NzIconModule,
    NzMessageModule,
    NzPopconfirmModule,
    NzButtonModule,
    NzAutocompleteModule,
    FormsModule,
    NzPopconfirmModule,
    NzTagModule,
    NzInputNumberModule,
    NzRadioModule
  ]
})
export class EmployerModule { }
