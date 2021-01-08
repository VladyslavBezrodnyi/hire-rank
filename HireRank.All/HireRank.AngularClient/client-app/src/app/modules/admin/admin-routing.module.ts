import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AdminProfileComponent} from './admin-profile/admin-profile.component';
import { AuthGuard} from '../../core/guards/auth.guard';
import { AdminCampaignsComponent } from './admin-campaigns/admin-campaigns.component';
import { CampaignRatingsComponent } from './campaign-ratings/campaign-ratings.component'
const routes: Routes = [
  { path: 'profile', component: AdminProfileComponent},
  { path: 'campaigns', component: AdminCampaignsComponent},
  { path: 'campaign/:id', component: CampaignRatingsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
