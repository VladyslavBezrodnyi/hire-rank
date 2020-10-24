import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AdminProfileComponent} from './admin-profile/admin-profile.component';
import { AuthGuard} from '../../core/guards/auth.guard';

const routes: Routes = [
  { path: 'profile', component: AdminProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
