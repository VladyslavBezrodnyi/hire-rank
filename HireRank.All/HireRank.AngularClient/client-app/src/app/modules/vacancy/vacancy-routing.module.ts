import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {VacanciesListComponent} from './vacancies-list/vacancies-list.component';
import {VacancyPageComponent} from './vacancy-page/vacancy-page.component';
import {VacancyTestComponent} from './vacancy-test/vacancy-test.component';

const routes: Routes = [
  { path: 'test/:id', component: VacancyTestComponent},
  { path: ':id', component: VacancyPageComponent},
  { path: '', component: VacanciesListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VacancyRoutingModule { }
