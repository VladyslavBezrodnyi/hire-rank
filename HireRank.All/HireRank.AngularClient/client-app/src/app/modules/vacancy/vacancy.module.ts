import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VacancyRoutingModule } from './vacancy-routing.module';
import { VacanciesListComponent } from './vacancies-list/vacancies-list.component';
import { NgZorroAntdModule } from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import { VacancyPageComponent } from './vacancy-page/vacancy-page.component';
import { VacancyTestComponent } from './vacancy-test/vacancy-test.component';
import {FormsModule} from '@angular/forms';

@NgModule({
  declarations: [VacanciesListComponent, VacancyPageComponent, VacancyTestComponent],
  imports: [
    CommonModule,
    VacancyRoutingModule,
    NgZorroAntdModule,
    FormsModule
  ]
})
export class VacancyModule { }
