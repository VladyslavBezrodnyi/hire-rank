import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminProfileComponent } from './admin-profile/admin-profile.component';
import {AdminRoutingModule} from './admin-routing.module';
import {NgZorroAntdModule} from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import {ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [AdminProfileComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgZorroAntdModule,
    ReactiveFormsModule
  ]
})
export class AdminModule { }
