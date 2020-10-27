import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {EmployerProfileComponent} from './employer-profile/employer-profile.component';
import { AuthGuard} from '../../core/guards/auth.guard';

const routes: Routes = [
  { path: 'profile', component: EmployerProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployerRoutingModule { }
