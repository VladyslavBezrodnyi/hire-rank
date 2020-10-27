import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {StudentProfileComponent} from './student-profile/student-profile.component';
import { AuthGuard} from '../../core/guards/auth.guard';

const routes: Routes = [
  { path: 'profile', component: StudentProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
