import { AccountRoutingModule } from './account-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { IconsProviderModule } from '../../icons-provider.module';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { ReactiveFormsModule } from '@angular/forms';
import { UserOutline, SendOutline, LockOutline } from '@ant-design/icons-angular/icons';
import { IconDefinition } from '@ant-design/icons-angular';

@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    CommonModule,
    NzModalModule,
    NzInputModule,
    NzFormModule,
    NzButtonModule,
    ReactiveFormsModule,
    IconsProviderModule,
    AccountRoutingModule
  ],
  exports: [
    LoginComponent
  ]

})
export class AccountModule { }
