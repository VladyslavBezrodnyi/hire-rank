import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VacanciesRoutingModule } from './vacancies-routing.module';
import { VacancyPageComponent } from './vacancy-page/vacancy-page.component';
import { VacancyTestingComponent } from './vacancy-testing/vacancy-testing.component';
import { VacancyListComponent } from './vacancy-list/vacancy-list.component';
import { NgZorroAntdModule } from 'src/app/shared/libraries/ng-zorro-antd/ng-zorro-antd.module';



@NgModule({
  declarations: [
    VacancyPageComponent,
    VacancyTestingComponent,
    VacancyListComponent
  ],
  imports: [
    CommonModule,
    VacanciesRoutingModule,
    NgZorroAntdModule
  ]
})
export class VacanciesModule { }
