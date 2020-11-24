import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EmployerRoutingModule} from './employer-routing.module';
import {NgZorroAntdModule} from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import { EmployerProfileComponent } from './employer-profile/employer-profile.component';
import {ReactiveFormsModule} from '@angular/forms';


@NgModule({
  declarations: [EmployerProfileComponent],
  imports: [
    CommonModule,
    NgZorroAntdModule,
    EmployerRoutingModule,
    ReactiveFormsModule
  ]
})
export class EmployerModule { }
