import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {EmployerProfileComponent} from './employer-profile/employer-profile.component';
import { AddTestsForVacancyComponent } from './add-tests-for-vacancy/add-tests-for-vacancy.component';

const routes: Routes = [
  { path: 'profile', component: EmployerProfileComponent},
  { path: 'vacancy/:id/tests', component: AddTestsForVacancyComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployerRoutingModule { }
