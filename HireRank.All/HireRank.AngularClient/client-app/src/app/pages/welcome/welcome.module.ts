import { NgModule } from '@angular/core';
import { WelcomeRoutingModule } from './welcome-routing.module';
import { WelcomeComponent } from './welcome.component';
import { NgZorroAntdModule } from '../../shared/libraries/ng-zorro-antd/ng-zorro-antd.module';
import {AccountModule} from '../../modules/account/account.module';

@NgModule({
  declarations: [WelcomeComponent],
  imports: [
    WelcomeRoutingModule,
    NgZorroAntdModule,
    AccountModule
  ],
  exports: [WelcomeComponent]
})
export class WelcomeModule { }
