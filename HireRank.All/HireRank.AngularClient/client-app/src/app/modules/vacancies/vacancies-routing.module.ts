import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard} from '../../core/guards/auth.guard';
import { VacancyListComponent } from './vacancy-list/vacancy-list.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: VacancyListComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VacanciesRoutingModule { }
